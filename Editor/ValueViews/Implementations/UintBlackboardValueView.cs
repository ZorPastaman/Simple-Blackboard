// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UintBlackboardValueView : BlackboardValueView<uint>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var uintField = new UintField(label);

			if (blackboardRoot != null)
			{
				uintField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), uintField.value);
					}
				});
			}

			return uintField;
		}

		public override void UpdateValue(VisualElement visualElement, uint value)
		{
			var uintField = (UintField)visualElement;
			uintField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var uintField = (UintField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), uintField.value);
		}

		public override uint DrawValue(string label, uint value)
		{
			return (uint)Math.Max(Math.Min(EditorGUILayout.LongField(label, value), uint.MaxValue), uint.MinValue);
		}
	}
}
