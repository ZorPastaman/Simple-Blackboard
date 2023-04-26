// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Vector3Int"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class Vector3IntBlackboardValueView : BlackboardValueView<Vector3Int>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Vector3Int> CreateBaseField(string label)
		{
			return new Vector3IntField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override Vector3Int DrawValue(string label, Vector3Int value)
		{
			return EditorGUILayout.Vector3IntField(label, value);
		}
	}
}
