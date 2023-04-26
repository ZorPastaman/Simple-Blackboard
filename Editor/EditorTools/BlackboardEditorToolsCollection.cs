// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Zor.SimpleBlackboard.EditorWindows;
using Zor.SimpleBlackboard.BlackboardValueViews;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.Debugging;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <summary>
	/// Static collection of blackboard editor tools.
	/// </summary>
	[InitializeOnLoad]
	internal static class BlackboardEditorToolsCollection
	{
		[NotNull] private static readonly MethodInfo s_addPopupSetupInfo = typeof(AddPopup).GetMethod("Setup")
			?? throw new InvalidOperationException();

		[NotNull] private static readonly Dictionary<Type, IBlackboardValueView> s_valueViews;
		[NotNull] private static readonly Dictionary<Type, BlackboardTableEditor_Base> s_tableEditors;

		static BlackboardEditorToolsCollection()
		{
			Type[] types = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
					where !domainAssembly.IsDynamic
					from assemblyType in domainAssembly.GetExportedTypes()
					where !assemblyType.IsAbstract && !assemblyType.IsGenericType
						&& typeof(IBlackboardValueView).IsAssignableFrom(assemblyType)
					select assemblyType)
				.ToArray();

			int count = types.Length;

			s_valueViews = new Dictionary<Type, IBlackboardValueView>(count);
			s_tableEditors = new Dictionary<Type, BlackboardTableEditor_Base>(count);

			for (int i = 0; i < count; ++i)
			{
				Type type = types[i];

				var valueView = (IBlackboardValueView)Activator.CreateInstance(type);
				Type valueViewType = valueView.valueType;
				s_valueViews[valueViewType] = valueView;

				Type tableEditorType = valueViewType.IsValueType
					? typeof(StructBlackboardTableEditor<>)
					: typeof(ClassBlackboardTableEditor<>);
				Type editorViewType = tableEditorType.MakeGenericType(valueViewType);
				var editorView = (BlackboardTableEditor_Base)Activator.CreateInstance(editorViewType, valueView);
				s_tableEditors[editorView.valueType] = editorView;
			}
		}

		/// <summary>
		/// Tries to get an editor for <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Value type for which an editor is needed.</param>
		/// <param name="blackboardTableEditor">
		/// Found table editor or <see langword="default"/> if it's not found.
		/// </param>
		/// <returns>True if the editor is found; false otherwise.</returns>
		[Pure]
		public static bool TryGetTableEditor([NotNull] Type valueType,
			[CanBeNull] out BlackboardTableEditor_Base blackboardTableEditor)
		{
			return s_tableEditors.TryGetValue(valueType, out blackboardTableEditor);
		}

		/// <summary>
		/// Adds all value types of existing <see cref="IBlackboardValueView"/>s to <paramref name="valueViewTypes"/>.
		/// </summary>
		/// <param name="valueViewTypes">
		/// Value types of existing <see cref="IBlackboardValueView"/>s are added to this.
		/// </param>
		public static void GetValueViewTypes([NotNull] List<Type> valueViewTypes)
		{
			valueViewTypes.AddRange(s_valueViews.Keys);
		}

		/// <summary>
		/// Creates and shows <see cref="AddPopup"/> for <paramref name="valueType"/> and <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="valueType">Value type of a property to add to <paramref name="blackboard"/>.</param>
		/// <param name="blackboard">A new property is added to this.</param>
		/// <param name="key">Initial key of the property.</param>
		/// <param name="position">Position of the popup.</param>
		/// <returns>
		/// Created <see cref="AddPopup"/>.
		/// May be <see langword="null"/> if a <see cref="IBlackboardValueView"/>
		/// for <paramref name="valueType"/> doesn't exist.
		/// </returns>
		[CanBeNull]
		public static AddPopup CreateAddPopup([NotNull] Type valueType, [NotNull] Blackboard blackboard,
			[NotNull] string key, Vector2 position)
		{
			if (!s_valueViews.TryGetValue(valueType, out IBlackboardValueView valueView))
			{
				BlackboardDebug.LogError($"Can't create an add popup of type {valueType.FullName} because there's no value view for that type.");
				return null;
			}

			var addPopup = ScriptableObject.CreateInstance<AddPopup>();
			MethodInfo method = s_addPopupSetupInfo.MakeGenericMethod(valueView.valueType);
			var parameters = new object[] {blackboard, key, valueView.CreateVisualElement(null), position};
			method.Invoke(addPopup, parameters);

			return addPopup;
		}
	}
}
