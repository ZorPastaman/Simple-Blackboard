// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Bool Accessor")]
	public sealed class BoolAccessor : StructAccessor<bool, BoolAccessor.BoolEvent>
	{
		[Serializable]
		public sealed class BoolEvent : UnityEvent<bool>
		{
		}
	}
}
