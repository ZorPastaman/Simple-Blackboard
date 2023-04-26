// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Vector2Int"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class Vector2IntBlackboardValueView : BlackboardValueView<Vector2Int>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Vector2Int> CreateBaseField(string label)
		{
			return new Vector2IntField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override Vector2Int DrawValue(string label, Vector2Int value)
		{
			return EditorGUILayout.Vector2IntField(label, value);
		}
	}
}
