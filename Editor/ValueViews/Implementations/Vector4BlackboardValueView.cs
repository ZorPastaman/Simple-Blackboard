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
	public sealed class Vector4BlackboardValueView : BlackboardValueView<Vector4, Vector4, Vector4Field>
	{
		public override Vector4Field CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var vector4Field = new Vector4Field(label);

			if (blackboardRoot != null)
			{
				vector4Field.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), vector4Field.value);
					}
				});
			}

			return vector4Field;
		}

		public override void UpdateValue(VisualElement visualElement, Vector4 value)
		{
			var vector4Field = (Vector4Field)visualElement;
			vector4Field.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var vector4Field = (Vector4Field)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), vector4Field.value);
		}

		public override Vector4 DrawValue(string label, Vector4 value)
		{
			return EditorGUILayout.Vector4Field(label, value);
		}
	}
}
