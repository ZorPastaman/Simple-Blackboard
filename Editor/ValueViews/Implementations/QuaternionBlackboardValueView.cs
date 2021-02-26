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
	public sealed class QuaternionBlackboardValueView : BlackboardValueView<Quaternion, Quaternion, QuaternionField>
	{
		public override QuaternionField CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var quaternionField = new QuaternionField(label);

			if (blackboardRoot != null)
			{
				quaternionField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), quaternionField.value);
					}
				});
			}

			return quaternionField;
		}

		public override void UpdateValue(VisualElement visualElement, Quaternion value)
		{
			var quaternionField = (QuaternionField)visualElement;
			quaternionField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var quaternionField = (QuaternionField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), quaternionField.value);
		}

		public override Quaternion DrawValue(string label, Quaternion value)
		{
			Vector4 vector = EditorGUILayout.Vector4Field(label, new Vector4(value.x, value.y, value.z, value.w));
			return new Quaternion(vector.x, vector.y, vector.z, vector.w);
		}
	}
}
