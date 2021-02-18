// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class StringBlackboardValueView : BlackboardValueView<string>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var textField = new TextField(label);

			if (blackboardRoot != null)
			{
				textField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetClassValue(new BlackboardPropertyName(label), textField.value);
					}
				});
			}

			return textField;
		}

		public override void UpdateValue(VisualElement visualElement, string value)
		{
			var textField = (TextField)visualElement;
			textField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var textField = (TextField)visualElement;
			blackboard.SetClassValue(new BlackboardPropertyName(key), textField.value);
		}

		public override string DrawValue(string label, string value)
		{
			return EditorGUILayout.TextField(label, value);
		}
	}
}
