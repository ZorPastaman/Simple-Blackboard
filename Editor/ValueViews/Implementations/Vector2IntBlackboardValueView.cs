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
	public sealed class Vector2IntBlackboardValueView : BlackboardValueView<Vector2Int>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new Vector2IntField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var vector2IntField = (Vector2IntField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), vector2IntField.value);
		}

		public override Vector2Int DrawValue(string label, Vector2Int value)
		{
			return EditorGUILayout.Vector2IntField(label, value);
		}
	}
}
