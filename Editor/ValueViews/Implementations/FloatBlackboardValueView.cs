// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class FloatBlackboardValueView : BlackboardValueView<float>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var floatField = new FloatField(label);

			if (blackboardRoot != null)
			{
				floatField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), floatField.value);
					}
				});
			}

			return floatField;
		}

		public override void UpdateValue(VisualElement visualElement, float value)
		{
			var floatField = (FloatField)visualElement;
			floatField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var floatField = (FloatField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), floatField.value);
		}

		public override float DrawValue(string label, float value)
		{
			return EditorGUILayout.FloatField(label, value);
		}
	}
}
