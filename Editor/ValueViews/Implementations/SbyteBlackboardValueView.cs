// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="sbyte"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class SbyteBlackboardValueView : BlackboardValueView<sbyte>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<sbyte> CreateBaseField(string label)
		{
			return new SbyteField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override sbyte DrawValue(string label, sbyte value)
		{
			return (sbyte)Mathf.Clamp(EditorGUILayout.IntField(label, value), sbyte.MinValue, sbyte.MaxValue);
		}
	}
}
