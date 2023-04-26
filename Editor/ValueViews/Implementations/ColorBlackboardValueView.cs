// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Color"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class ColorBlackboardValueView : BlackboardValueView<Color>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Color> CreateBaseField(string label)
		{
			return new ColorField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override Color DrawValue(string label, Color value)
		{
			return EditorGUILayout.ColorField(label, value);
		}
	}
}
