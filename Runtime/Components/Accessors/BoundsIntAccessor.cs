// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Bounds Int Accessor")]
	public sealed class BoundsIntAccessor : StructAccessor<BoundsInt, BoundsIntAccessor.BoundsIntEvent>
	{
		[Serializable]
		public sealed class BoundsIntEvent : UnityEvent<BoundsInt>
		{
		}
	}
}
