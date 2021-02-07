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
	public sealed class RectBlackboardValueView : BlackboardValueView<Rect>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new RectField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var rectField = (RectField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), rectField.value);
		}

		public override Rect DrawValue(string label, Rect value)
		{
			return EditorGUILayout.RectField(label, value);
		}
	}
}
