// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Animation Curve Accessor")]
	public abstract class AnimationCurveAccessor :
		ClassAccessor<AnimationCurve, AnimationCurveAccessor.AnimationCurveEvent>
	{
		[Serializable]
		public sealed class AnimationCurveEvent : UnityEvent<AnimationCurve>
		{
		}
	}
}
