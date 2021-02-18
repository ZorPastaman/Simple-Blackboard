// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UintBlackboardValueView : BlackboardValueView<uint>
	{
		private static readonly EventCallback<ChangeEvent<long>> s_onValueChanged = OnValueChanged;

		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var longField = new LongField(label);
			longField.RegisterValueChangedCallback(s_onValueChanged);

			if (blackboardRoot != null)
			{
				longField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), (uint)longField.value);
					}
				});
			}

			return longField;
		}

		public override void UpdateValue(VisualElement visualElement, uint value)
		{
			var longField = (LongField)visualElement;
			longField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var longField = (LongField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), (uint)longField.value);
		}

		public override uint DrawValue(string label, uint value)
		{
			return (uint)Math.Max(Math.Min(EditorGUILayout.LongField(label, value), uint.MaxValue), uint.MinValue);
		}

		private static void OnValueChanged([NotNull] ChangeEvent<long> changeEvent)
		{
			var longField = (LongField)changeEvent.target;
			longField.value = Math.Max(Math.Min(longField.value, uint.MaxValue), uint.MinValue);
		}
	}
}
