// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UlongBlackboardValueView : BlackboardValueView<ulong>
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
						blackboard.SetStructValue(new BlackboardPropertyName(label), (ulong)longField.value);
					}
				});
			}

			return longField;
		}

		public override void UpdateValue(VisualElement visualElement, ulong value)
		{
			var longField = (LongField)visualElement;
			longField.value = (long)value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var longField = (LongField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), (ulong)longField.value);
		}

		public override ulong DrawValue(string label, ulong value)
		{
			long result = EditorGUILayout.LongField(label, (long)value);

			if (result < (long)ulong.MinValue)
			{
				result = (long)ulong.MinValue;
			}

			return (ulong)result;
		}

		private static void OnValueChanged([NotNull] ChangeEvent<long> changeEvent)
		{
			var longField = (LongField)changeEvent.target;

			if (longField.value < (long)ulong.MinValue)
			{
				longField.value = (long)ulong.MinValue;
			}
		}
	}
}
