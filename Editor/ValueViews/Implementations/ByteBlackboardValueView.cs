// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="byte"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class ByteBlackboardValueView : BlackboardValueView<byte>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<byte> CreateBaseField(string label)
		{
			return new ByteField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override byte DrawValue(string label, byte value)
		{
			return (byte)Mathf.Clamp(EditorGUILayout.IntField(label, value), byte.MinValue, byte.MaxValue);
		}
	}
}
