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
	public sealed class RectIntBlackboardValueView : BlackboardValueView<RectInt>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var rectIntField = new RectIntField(label);

			if (blackboardRoot != null)
			{
				rectIntField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), rectIntField.value);
					}
				});
			}

			return rectIntField;
		}

		public override void UpdateValue(VisualElement visualElement, RectInt value)
		{
			var rectIntField = (RectIntField)visualElement;
			rectIntField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var rectIntField = (RectIntField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), rectIntField.value);
		}

		public override RectInt DrawValue(string label, RectInt value)
		{
			return EditorGUILayout.RectIntField(label, value);
		}
	}
}
