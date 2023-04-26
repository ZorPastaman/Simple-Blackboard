// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="bool"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class BoolBlackboardValueView : BlackboardValueView<bool>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<bool> CreateBaseField(string label)
		{
			return new Toggle(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override bool DrawValue(string label, bool value)
		{
			return EditorGUILayout.Toggle(label, value);
		}
	}
}
