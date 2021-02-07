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
	public sealed class UshortBlackboardValueView : BlackboardValueView<ushort>
	{
		private static readonly EventCallback<ChangeEvent<int>> s_onValueChanged = OnValueChanged;

		public override VisualElement CreateVisualElement(string label)
		{
			var intField = new IntegerField(label);
			intField.RegisterValueChangedCallback(s_onValueChanged);

			return intField;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var intField = (IntegerField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), (ushort)intField.value);
		}

		public override ushort DrawValue(string label, ushort value)
		{
			return (ushort)Mathf.Clamp(EditorGUILayout.IntField(label, value), ushort.MinValue, ushort.MaxValue);
		}

		private static void OnValueChanged([NotNull] ChangeEvent<int> changeEvent)
		{
			var intField = (IntegerField)changeEvent.target;
			intField.value = Mathf.Clamp(intField.value, ushort.MinValue, ushort.MaxValue);
		}
	}
}
