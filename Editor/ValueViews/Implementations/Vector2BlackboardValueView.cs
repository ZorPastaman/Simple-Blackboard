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
	public sealed class Vector2BlackboardValueView : BlackboardValueView<Vector2>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var vector2Field = new Vector2Field(label);

			if (blackboardRoot != null)
			{
				vector2Field.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), vector2Field.value);
					}
				});
			}

			return vector2Field;
		}

		public override void UpdateValue(VisualElement visualElement, Vector2 value)
		{
			var vector2Field = (Vector2Field)visualElement;
			vector2Field.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var vector2Field = (Vector2Field)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), vector2Field.value);
		}

		public override Vector2 DrawValue(string label, Vector2 value)
		{
			return EditorGUILayout.Vector2Field(label, value);
		}
	}
}
