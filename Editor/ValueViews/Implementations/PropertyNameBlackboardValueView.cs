// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="PropertyName"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class PropertyNameBlackboardValueView : BlackboardValueView<PropertyName>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<PropertyName> CreateBaseField(string label)
		{
			return new PropertyNameField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override PropertyName DrawValue(string label, PropertyName value)
		{
			string stringValue = value.ToString();
			stringValue = stringValue.Substring(0, stringValue.IndexOf(':'));
			stringValue = EditorGUILayout.TextField(label, stringValue);
			return new PropertyName(stringValue);
		}
	}
}
