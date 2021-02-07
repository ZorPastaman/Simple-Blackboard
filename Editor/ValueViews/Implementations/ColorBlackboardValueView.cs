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
		public override VisualElement CreateVisualElement(string label)
		{
			return new ColorField(label);
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
