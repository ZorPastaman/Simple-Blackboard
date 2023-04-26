// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="long"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class LongBlackboardValueView : BlackboardValueView<long>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<long> CreateBaseField(string label)
		{
			return new LongField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override long DrawValue(string label, long value)
		{
			return EditorGUILayout.LongField(label, value);
		}
	}
}
