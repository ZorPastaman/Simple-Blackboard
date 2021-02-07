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
	public sealed class BoundsIntBlackboardValueView : BlackboardValueView<BoundsInt>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new BoundsIntField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var boundsIntField = (BoundsIntField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), boundsIntField.value);
		}

		public override BoundsInt DrawValue(string label, BoundsInt value)
		{
			return EditorGUILayout.BoundsIntField(label, value);
		}
	}
}
