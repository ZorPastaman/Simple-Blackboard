// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class ShortBlackboardValueView : BlackboardValueView<short, short, ShortField>
	{
		public override ShortField CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var shortField = new ShortField(label);

			if (blackboardRoot != null)
			{
				shortField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), shortField.value);
					}
				});
			}

			return shortField;
		}

		public override void UpdateValue(VisualElement visualElement, short value)
		{
			var shortField = (ShortField)visualElement;
			shortField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var shortField = (ShortField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), shortField.value);
		}

		public override short DrawValue(string label, short value)
		{
			return (short)Mathf.Clamp(EditorGUILayout.IntField(label, value), short.MinValue, short.MaxValue);
		}
	}
}
