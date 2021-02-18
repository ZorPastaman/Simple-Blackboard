// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class DoubleBlackboardValueView : BlackboardValueView<double>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var doubleField = new DoubleField(label);

			if (blackboardRoot != null)
			{
				doubleField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), doubleField.value);
					}
				});
			}

			return doubleField;
		}

		public override void UpdateValue(VisualElement visualElement, double value)
		{
			var doubleField = (DoubleField)visualElement;
			doubleField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var doubleField = (DoubleField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), doubleField.value);
		}

		public override double DrawValue(string label, double value)
		{
			return EditorGUILayout.DoubleField(label, value);
		}
	}
}
