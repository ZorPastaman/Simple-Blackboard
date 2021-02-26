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
	public sealed class Vector3BlackboardValueView : BlackboardValueView<Vector3, Vector3, Vector3Field>
	{
		public override Vector3Field CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var vector3Field = new Vector3Field(label);

			if (blackboardRoot != null)
			{
				vector3Field.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), vector3Field.value);
					}
				});
			}

			return vector3Field;
		}

		public override void UpdateValue(VisualElement visualElement, Vector3 value)
		{
			var vector3Field = (Vector3Field)visualElement;
			vector3Field.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var vector3Field = (Vector3Field)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), vector3Field.value);
		}

		public override Vector3 DrawValue(string label, Vector3 value)
		{
			return EditorGUILayout.Vector3Field(label, value);
		}
	}
}
