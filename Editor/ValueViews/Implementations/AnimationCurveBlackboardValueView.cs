// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="AnimationCurve"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class AnimationCurveBlackboardValueView : BlackboardValueView<AnimationCurve>
	{
		/// <inheritdoc/>
		public override BaseField<AnimationCurve> CreateBaseField(string label)
		{
			return new FastCurveField(label);
		}

		/// <inheritdoc/>
		public override AnimationCurve DrawValue(string label, AnimationCurve value)
		{
			if (value == null)
			{
				value = new AnimationCurve();
			}

			return EditorGUILayout.CurveField(label, value);
		}
	}
}
