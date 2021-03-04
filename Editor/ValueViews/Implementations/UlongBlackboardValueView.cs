// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="ulong"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class UlongBlackboardValueView : BlackboardValueView<ulong>
	{
		/// <inheritdoc/>
		public override BaseField<ulong> CreateBaseField(string label)
		{
			return new UlongField(label);
		}

		/// <inheritdoc/>
		public override ulong DrawValue(string label, ulong value)
		{
			long result = EditorGUILayout.LongField(label, (long)value);

			if (result < (long)ulong.MinValue)
			{
				result = (long)ulong.MinValue;
			}

			return (ulong)result;
		}
	}
}
