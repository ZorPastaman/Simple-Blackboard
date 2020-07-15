// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Rect Int Accessor")]
	public sealed class RectIntAccessor : StructAccessor<RectInt, RectIntAccessor.RectIntEvent>
	{
		[Serializable]
		public sealed class RectIntEvent : UnityEvent<RectInt>
		{
		}
	}
}
