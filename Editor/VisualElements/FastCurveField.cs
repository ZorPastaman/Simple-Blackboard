// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a field for editing an AnimationCurve that doesn't copy input into an internal field.
	/// </summary>
	public sealed class FastCurveField : CurveField
	{
		public FastCurveField() : base()
		{
		}

		public FastCurveField([CanBeNull] string label) : base(label)
		{
		}

		public override AnimationCurve value
		{
			get => rawValue;
			set
			{
				rawValue = value ?? new AnimationCurve(new Keyframe[0]);
				base.value = value;
			}
		}

		public override void SetValueWithoutNotify(AnimationCurve newValue)
		{
			if (newValue != null)
			{
				rawValue = newValue;
			}

			base.SetValueWithoutNotify(newValue);
		}
	}
}
