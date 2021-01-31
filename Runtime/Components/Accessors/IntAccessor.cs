// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Int Accessor")]
	public sealed class IntAccessor : StructAccessor<int, IntAccessor.IntEvent>
	{
		[Serializable]
		public sealed class IntEvent : UnityEvent<int>
		{
		}
	}
}
