// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="short"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class ShortBlackboardValueView : BlackboardValueView<short>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<short> CreateBaseField(string label)
		{
			return new ShortField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override short DrawValue(string label, short value)
		{
			return (short)Mathf.Clamp(EditorGUILayout.IntField(label, value), short.MinValue, short.MaxValue);
		}
	}
}
