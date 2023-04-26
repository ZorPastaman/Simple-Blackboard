// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Double Accessor")]
	public sealed class DoubleAccessor : StructAccessor<double, DoubleAccessor.DoubleEvent>
	{
		[Serializable]
		public sealed class DoubleEvent : UnityEvent<double>
		{
		}
	}
}
