// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Rect"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class RectBlackboardValueView : BlackboardValueView<Rect>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Rect> CreateBaseField(string label)
		{
			return new RectField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override Rect DrawValue(string label, Rect value)
		{
			return EditorGUILayout.RectField(label, value);
		}
	}
}
