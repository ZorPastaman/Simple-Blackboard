// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Quaternion"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class QuaternionBlackboardValueView : BlackboardValueView<Quaternion>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Quaternion> CreateBaseField(string label)
		{
			return new QuaternionField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override Quaternion DrawValue(string label, Quaternion value)
		{
			Vector4 vector = EditorGUILayout.Vector4Field(label, new Vector4(value.x, value.y, value.z, value.w));
			return new Quaternion(vector.x, vector.y, vector.z, vector.w);
		}
	}
}
