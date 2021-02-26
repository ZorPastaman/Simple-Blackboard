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
	public sealed class UshortBlackboardValueView : BlackboardValueView<ushort, ushort, UshortField>
	{
		public override UshortField CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var ushortField = new UshortField(label);

			if (blackboardRoot != null)
			{
				ushortField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), ushortField.value);
					}
				});
			}

			return ushortField;
		}

		public override void UpdateValue(VisualElement visualElement, ushort value)
		{
			var ushortField = (UshortField)visualElement;
			ushortField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var ushortField = (UshortField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), ushortField.value);
		}

		public override ushort DrawValue(string label, ushort value)
		{
			return (ushort)Mathf.Clamp(EditorGUILayout.IntField(label, value), ushort.MinValue, ushort.MaxValue);
		}
	}
}
