// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="RectInt"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class RectIntBlackboardValueView : BlackboardValueView<RectInt>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<RectInt> CreateBaseField(string label)
		{
			return new RectIntField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override RectInt DrawValue(string label, RectInt value)
		{
			return EditorGUILayout.RectIntField(label, value);
		}
	}
}
