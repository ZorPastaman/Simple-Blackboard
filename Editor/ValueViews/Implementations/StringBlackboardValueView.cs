// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="string"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class StringBlackboardValueView : BlackboardValueView<string>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<string> CreateBaseField(string label)
		{
			return new TextField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override string DrawValue(string label, string value)
		{
			return EditorGUILayout.TextField(label, value);
		}
	}
}
