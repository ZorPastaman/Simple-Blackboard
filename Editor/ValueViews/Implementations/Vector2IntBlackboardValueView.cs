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
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var vector2IntField = new Vector2IntField(label);

			if (blackboardRoot != null)
			{
				vector2IntField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), vector2IntField.value);
					}
				});
			}

			return vector2IntField;
		}

		public override void UpdateValue(VisualElement visualElement, Vector2Int value)
		{
			var vector2IntField = (Vector2IntField)visualElement;
			vector2IntField.value = value;
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
