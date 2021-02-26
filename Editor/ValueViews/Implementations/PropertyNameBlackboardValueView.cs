// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class PropertyNameBlackboardValueView : BlackboardValueView<PropertyName, PropertyName, PropertyNameField>
	{
		public override PropertyNameField CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var propertyNameField = new PropertyNameField(label);

			if (blackboardRoot != null)
			{
				propertyNameField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), propertyNameField.value);
					}
				});
			}

			return propertyNameField;
		}

		public override void UpdateValue(VisualElement visualElement, PropertyName value)
		{
			var propertyNameField = (PropertyNameField)visualElement;
			propertyNameField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var propertyNameField = (PropertyNameField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), propertyNameField.value);
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
