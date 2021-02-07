// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector4BlackboardValueView : BlackboardValueView<Vector4>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new Vector4Field(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var vector4Field = (Vector4Field)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), vector4Field.value);
		}

		public override Vector4 DrawValue(string label, Vector4 value)
		{
			return EditorGUILayout.Vector4Field(label, value);
		}
	}
}
