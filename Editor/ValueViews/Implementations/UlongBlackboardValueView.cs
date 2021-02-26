// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UlongBlackboardValueView : BlackboardValueView<ulong, ulong, UlongField>
	{
		public override UlongField CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var ulongField = new UlongField(label);

			if (blackboardRoot != null)
			{
				ulongField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), ulongField.value);
					}
				});
			}

			return ulongField;
		}

		public override void UpdateValue(VisualElement visualElement, ulong value)
		{
			var longField = (UlongField)visualElement;
			longField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var longField = (UlongField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), longField.value);
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
	}
}
