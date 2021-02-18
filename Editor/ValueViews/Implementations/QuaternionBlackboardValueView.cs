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
	public sealed class QuaternionBlackboardValueView : BlackboardValueView<Quaternion>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var vector4Field = new Vector4Field(label);

			if (blackboardRoot != null)
			{
				vector4Field.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						Vector4 vector = vector4Field.value;

						blackboard.SetStructValue(new BlackboardPropertyName(label),
							new Quaternion(vector.x, vector.y, vector.z, vector.w));
					}
				});
			}

			return vector4Field;
		}

		public override void UpdateValue(VisualElement visualElement, Quaternion value)
		{
			var vector4Field = (Vector4Field)visualElement;
			vector4Field.value = new Vector4(value.x, value.y, value.z, value.w);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var vectorField = (Vector4Field)visualElement;
			Vector4 value = vectorField.value;
			var quaternion = new Quaternion(value.x, value.y, value.z, value.w);
			blackboard.SetStructValue(new BlackboardPropertyName(key), quaternion);
		}

		public override Quaternion DrawValue(string label, Quaternion value)
		{
			Vector4 vector =
				EditorGUILayout.Vector4Field(label, new Vector4(value.x, value.y, value.z, value.w));

			return new Quaternion(vector.x, vector.y, vector.z, vector.w);
		}
	}
}
