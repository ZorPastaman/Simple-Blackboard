// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Zor.SimpleBlackboard.BlackboardValueViews;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <summary>
	/// Draws an editor for <see cref="BlackboardTable{T}"/>.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	internal abstract class BlackboardTableEditor<T> : BlackboardTableEditor_Base
	{
		private static readonly List<KeyValuePair<BlackboardPropertyName, T>> s_properties =
			new List<KeyValuePair<BlackboardPropertyName, T>>();

		private readonly BlackboardValueView<T> m_blackboardValueView;

		/// <summary>
		/// Creates a <see cref="BlackboardTableEditor{T}"/> using <paramref name="blackboardValueView"/> for drawing.
		/// </summary>
		/// <param name="blackboardValueView">
		/// This is used for drawing a property in <see cref="BlackboardTable{T}"/>
		/// </param>
		protected BlackboardTableEditor(BlackboardValueView<T> blackboardValueView)
		{
			m_blackboardValueView = blackboardValueView;
		}

		/// <inheritdoc/>
		public override Type valueType => typeof(T);

		/// <inheritdoc/>
		public override void Draw(Blackboard blackboard)
		{
			try
			{
				EditorGUILayout.LabelField(valueType.Name, EditorStyles.boldLabel);

				GetProperties(blackboard, s_properties);
				s_properties.Sort((left, right)
					=> string.CompareOrdinal(left.Key.name, right.Key.name));

				for (int i = 0, count = s_properties.Count; i < count; ++i)
				{
					KeyValuePair<BlackboardPropertyName, T> property = s_properties[i];

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.BeginVertical();

					EditorGUI.BeginChangeCheck();

					BlackboardPropertyName key = property.Key;
					T newValue = m_blackboardValueView.DrawValue(key.name, property.Value);

					if (EditorGUI.EndChangeCheck())
					{
						SetValue(blackboard, key, newValue);
					}

					EditorGUILayout.EndVertical();

					if (GUILayout.Button(s_RemoveButtonIcon, s_RemoveButtonOptions))
					{
						blackboard.RemoveObject<T>(key);
					}

					EditorGUILayout.EndHorizontal();
				}
			}
			finally
			{
				s_properties.Clear();
			}
		}

		/// <summary>
		/// Gets properties of the type <typeparamref name="T"/> from <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">Blackboard source.</param>
		/// <param name="properties">Properties output.</param>
		protected abstract void GetProperties([NotNull] Blackboard blackboard,
			[NotNull] List<KeyValuePair<BlackboardPropertyName, T>> properties);

		/// <summary>
		/// Sets <paramref name="value"/> into <paramref name="blackboard"/>
		/// with property name <paramref name="key"/>.
		/// </summary>
		/// <param name="blackboard">Property is set into this.</param>
		/// <param name="key">Property name.</param>
		/// <param name="value">Property value.</param>
		protected abstract void SetValue([NotNull] Blackboard blackboard,
			BlackboardPropertyName key, T value);
	}
}
