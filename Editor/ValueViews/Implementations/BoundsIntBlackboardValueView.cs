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
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var boundsIntField = new BoundsIntField(label);

			if (blackboardRoot != null)
			{
				boundsIntField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), boundsIntField.value);
					}
				});
			}

			return boundsIntField;
		}

		public override void UpdateValue(VisualElement visualElement, BoundsInt value)
		{
			var boundsIntField = (BoundsIntField)visualElement;
			boundsIntField.value = value;
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
