// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Matrix4x4"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class Matrix4x4BlackboardValueView : BlackboardValueView<Matrix4x4>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Matrix4x4> CreateBaseField(string label)
		{
			return new Matrix4x4Field(label);
		}

		/// <inheritdoc/>
		[Pure]
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
