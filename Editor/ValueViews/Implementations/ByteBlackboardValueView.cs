// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class ByteBlackboardValueView : BlackboardValueView<byte>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var byteField = new ByteField(label);

			if (blackboardRoot != null)
			{
				byteField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), byteField.value);
					}
				});
			}

			return byteField;
		}

		public override void UpdateValue(VisualElement visualElement, byte value)
		{
			var byteField = (ByteField)visualElement;
			byteField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var byteField = (ByteField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), byteField.value);
		}

		public override byte DrawValue(string label, byte value)
		{
			return (byte)Mathf.Clamp(EditorGUILayout.IntField(label, value), byte.MinValue, byte.MaxValue);
		}
	}
}
