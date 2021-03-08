// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="uint"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class UintBlackboardValueView : BlackboardValueView<uint>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<uint> CreateBaseField(string label)
		{
			return new UintField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override uint DrawValue(string label, uint value)
		{
			return (uint)Math.Max(Math.Min(EditorGUILayout.LongField(label, value), uint.MaxValue), uint.MinValue);
		}
	}
}
