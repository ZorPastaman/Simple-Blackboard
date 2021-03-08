// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="ushort"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class UshortBlackboardValueView : BlackboardValueView<ushort>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<ushort> CreateBaseField(string label)
		{
			return new UshortField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override ushort DrawValue(string label, ushort value)
		{
			return (ushort)Mathf.Clamp(EditorGUILayout.IntField(label, value), ushort.MinValue, ushort.MaxValue);
		}
	}
}
