// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Vector2"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class Vector2BlackboardValueView : BlackboardValueView<Vector2>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Vector2> CreateBaseField(string label)
		{
			return new Vector2Field(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override Vector2 DrawValue(string label, Vector2 value)
		{
			return EditorGUILayout.Vector2Field(label, value);
		}
	}
}
