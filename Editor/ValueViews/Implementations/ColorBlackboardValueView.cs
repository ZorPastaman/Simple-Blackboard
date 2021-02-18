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
	public sealed class ColorBlackboardValueView : BlackboardValueView<Color>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var colorField = new ColorField(label);

			if (blackboardRoot != null)
			{
				colorField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), colorField.value);
					}
				});
			}

			return colorField;
		}

		public override void UpdateValue(VisualElement visualElement, Color value)
		{
			var colorField = (ColorField)visualElement;
			colorField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var colorField = (ColorField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), colorField.value);
		}

		public override Color DrawValue(string label, Color value)
		{
			return EditorGUILayout.ColorField(label, value);
		}
	}
}
