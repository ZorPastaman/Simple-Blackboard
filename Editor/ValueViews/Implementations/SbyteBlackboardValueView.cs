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
	public sealed class SbyteBlackboardValueView : BlackboardValueView<sbyte, sbyte, SbyteField>
	{
		public override SbyteField CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var sbyteField = new SbyteField(label);

			if (blackboardRoot != null)
			{
				sbyteField.RegisterValueChangedCallback(c =>
				{
					if (sbyteField.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), sbyteField.value);
					}
				});
			}

			return sbyteField;
		}

		public override void UpdateValue(VisualElement visualElement, sbyte value)
		{
			var sbyteField = (SbyteField)visualElement;
			sbyteField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var sbyteField = (SbyteField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), sbyteField.value);
		}

		public override sbyte DrawValue(string label, sbyte value)
		{
			return (sbyte)Mathf.Clamp(EditorGUILayout.IntField(label, value), sbyte.MinValue, sbyte.MaxValue);
		}
	}
}
