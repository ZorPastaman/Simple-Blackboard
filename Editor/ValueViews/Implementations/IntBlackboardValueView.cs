// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class IntBlackboardValueView : BlackboardValueView<int>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var intField = new IntegerField(label);

			if (blackboardRoot != null)
			{
				intField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), intField.value);
					}
				});
			}

			return intField;
		}

		public override void UpdateValue(VisualElement visualElement, int value)
		{
			var intField = (IntegerField)visualElement;
			intField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var intField = (IntegerField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), intField.value);
		}

		public override int DrawValue(string label, int value)
		{
			return EditorGUILayout.IntField(label, value);
		}
	}
}
