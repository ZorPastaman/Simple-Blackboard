// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Ulong Accessor")]
	public sealed class UlongAccessor : StructAccessor<ulong, UlongAccessor.UlongEvent>
	{
		[Serializable]
		public sealed class UlongEvent : UnityEvent<ulong>
		{
		}
	}
}
