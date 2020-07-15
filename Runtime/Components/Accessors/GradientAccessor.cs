// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Gradient Accessor")]
	public sealed class GradientAccessor : ClassAccessor<Gradient, GradientAccessor.GradientEvent>
	{
		[Serializable]
		public sealed class GradientEvent : UnityEvent<Gradient>
		{
		}
	}
}
