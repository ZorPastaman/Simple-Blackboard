// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Long Accessor")]
	public sealed class LongAccessor : StructAccessor<long, LongAccessor.LongEvent>
	{
		[Serializable]
		public sealed class LongEvent : UnityEvent<long>
		{
		}
	}
}
