// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Gradient"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class GradientBlackboardValueView : BlackboardValueView<Gradient>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Gradient> CreateBaseField(string label)
		{
			return new GradientField(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override Gradient DrawValue(string label, Gradient value)
		{
			if (value == null)
			{
				value = new Gradient();
			}

			return EditorGUILayout.GradientField(label, value);
		}
	}
}
