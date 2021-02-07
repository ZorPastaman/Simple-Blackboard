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
	public sealed class GradientBlackboardValueView : BlackboardValueView<Gradient>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new GradientField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var gradientField = (GradientField)visualElement;
			blackboard.SetClassValue(new BlackboardPropertyName(key), gradientField.value);
		}

		public override Gradient DrawValue(string label, Gradient value)
		{
			if (value == null)
			{
				value = new Gradient();
			}

			return EditorGUILayout.GradientField(label, value);
		}
	}
}
