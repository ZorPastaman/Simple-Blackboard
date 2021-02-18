// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class CharBlackboardValueView : BlackboardValueView<char>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var textField = new TextField(label, 1, false, false, '*');

			if (blackboardRoot != null)
			{
				textField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						string result = textField.value;
						blackboard.SetStructValue(new BlackboardPropertyName(label), !string.IsNullOrEmpty(result)
							? result[0]
							: default);
					}
				});
			}

			return textField;
		}

		public override void UpdateValue(VisualElement visualElement, char value)
		{
			var textField = (TextField)visualElement;
			textField.value = value.ToString();
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var textField = (TextField)visualElement;
			string value = textField.value;
			blackboard.SetStructValue(new BlackboardPropertyName(key),
				!string.IsNullOrEmpty(value) ? textField.value[0] : default);
		}

		public override char DrawValue(string label, char value)
		{
			string result = EditorGUILayout.TextField(label, value.ToString());
			return !string.IsNullOrEmpty(result) ? result[0] : default;
		}
	}
}
