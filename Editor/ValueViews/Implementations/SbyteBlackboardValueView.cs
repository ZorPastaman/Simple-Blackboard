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
	public sealed class SbyteBlackboardValueView : BlackboardValueView<sbyte>
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
			blackboard.SetStructValue(new BlackboardPropertyName(key), (sbyte)intField.value);
		}

		public override sbyte DrawValue(string label, sbyte value)
		{
			return (sbyte)Mathf.Clamp(EditorGUILayout.IntField(label, value), sbyte.MinValue, sbyte.MaxValue);
		}

		private static void OnValueChanged([NotNull] ChangeEvent<int> changeEvent)
		{
			var intField = (IntegerField)changeEvent.target;
			intField.value = Mathf.Clamp(intField.value, sbyte.MinValue, sbyte.MaxValue);
		}
	}
}
