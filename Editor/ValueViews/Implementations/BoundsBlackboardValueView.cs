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
	public sealed class BoundsBlackboardValueView : BlackboardValueView<Bounds>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new BoundsField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var boundsField = (BoundsField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), boundsField.value);
		}

		public override Bounds DrawValue(string label, Bounds value)
		{
			return EditorGUILayout.BoundsField(label, value);
		}
	}
}
