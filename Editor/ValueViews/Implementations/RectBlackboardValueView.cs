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
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var rectField = new RectField(label);

			if (blackboardRoot != null)
			{
				rectField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), rectField.value);
					}
				});
			}

			return rectField;
		}

		public override void UpdateValue(VisualElement visualElement, Rect value)
		{
			var rectField = (RectField)visualElement;
			rectField.value = value;
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
