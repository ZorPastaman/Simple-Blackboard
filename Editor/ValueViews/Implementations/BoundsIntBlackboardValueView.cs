// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="BoundsInt"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class BoundsIntBlackboardValueView : BlackboardValueView<BoundsInt>
	{
		/// <inheritdoc/>
		public override BaseField<BoundsInt> CreateBaseField(string label)
		{
			return new BoundsIntField(label);
		}

		/// <inheritdoc/>
		public override BoundsInt DrawValue(string label, BoundsInt value)
		{
			return EditorGUILayout.BoundsIntField(label, value);
		}
	}
}
