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
	public sealed class Matrix4x4BlackboardValueView : BlackboardValueView<Matrix4x4>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			var matrixField = new VisualElement();
			IStyle matrixFieldStyle = matrixField.style;
			matrixFieldStyle.flexDirection = FlexDirection.Row;
			matrixFieldStyle.justifyContent = Justify.SpaceBetween;

			var labelElement = new Label(label);
			labelElement.style.marginLeft = 3f;
			matrixField.Add(labelElement);
			var valueElement = new VisualElement();
			valueElement.style.flexGrow = 0.73f;
			matrixField.Add(valueElement);

			for (int i = 0; i < 4; ++i)
			{
				var row = new Vector4Field {name = $"row {i.ToString()}"};
				valueElement.Add(row);
			}

			return matrixField;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var matrix = new Matrix4x4();

			for (int i = 0; i < 4; ++i)
			{
				matrix.SetRow(i, visualElement.Q<Vector4Field>($"row {i.ToString()}").value);
			}

			blackboard.SetStructValue(new BlackboardPropertyName(key), matrix);
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
