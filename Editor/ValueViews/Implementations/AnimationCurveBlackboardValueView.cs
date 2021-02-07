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
	public sealed class AnimationCurveBlackboardValueView : BlackboardValueView<AnimationCurve>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new CurveField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var curveField = (CurveField)visualElement;
			blackboard.SetClassValue(new BlackboardPropertyName(key), curveField.value);
		}

		public override AnimationCurve DrawValue(string label, AnimationCurve value)
		{
			if (value == null)
			{
				value = new AnimationCurve();
			}

			return EditorGUILayout.CurveField(label, value);
		}
	}
}
