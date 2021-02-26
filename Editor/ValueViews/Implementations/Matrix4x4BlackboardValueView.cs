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
	public sealed class Matrix4x4BlackboardValueView : BlackboardValueView<Matrix4x4>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var matrixField = new Matrix4x4Field(label);

			if (blackboardRoot != null)
			{
				matrixField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), matrixField.value);
					}
				});
			}

			return matrixField;
		}

		public override void UpdateValue(VisualElement visualElement, Matrix4x4 value)
		{
			var matrixField = (Matrix4x4Field)visualElement;
			matrixField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var matrixField = (Matrix4x4Field)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), matrixField.value);
		}

		public override Matrix4x4 DrawValue(string label, Matrix4x4 value)
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.PrefixLabel(label);

			EditorGUILayout.BeginVertical();

			for (int i = 0; i < 4; ++i)
			{
				EditorGUILayout.BeginHorizontal();

				Vector4 row = value.GetRow(i);

				for (int j = 0; j < 4; j++)
				{
					row[j] = EditorGUILayout.FloatField(row[j]);
				}

				value.SetRow(i, row);

				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();

			EditorGUILayout.EndHorizontal();

			return value;
		}
	}
}
