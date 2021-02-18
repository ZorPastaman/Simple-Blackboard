// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class PropertyNameBlackboardValueView : BlackboardValueView<PropertyName>
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
						blackboard.SetStructValue(new BlackboardPropertyName(label), new PropertyName(textField.value));
					}
				});
			}

			return textField;
		}

		public override void UpdateValue(VisualElement visualElement, PropertyName value)
		{
			var textField = (TextField)visualElement;
			string stringValue = value.ToString();
			textField.value = stringValue.Substring(0, stringValue.IndexOf(':'));
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var textField = (TextField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), new PropertyName(textField.value));
		}

		public override PropertyName DrawValue(string label, PropertyName value)
		{
			string stringValue = value.ToString();
			stringValue = stringValue.Substring(0, stringValue.IndexOf(':'));
			stringValue = EditorGUILayout.TextField(label, stringValue);
			return new PropertyName(stringValue);
		}
	}
}
