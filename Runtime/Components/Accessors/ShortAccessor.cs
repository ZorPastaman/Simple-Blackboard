// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Short Accessor")]
	public sealed class ShortAccessor : StructAccessor<short, ShortAccessor.ShortEvent>
	{
		[Serializable]
		public sealed class ShortEvent : UnityEvent<short>
		{
		}
	}
}
