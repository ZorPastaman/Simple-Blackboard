// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Uint Accessor")]
	public sealed class UintAccessor : StructAccessor<uint, UintAccessor.UintEvent>
	{
		[Serializable]
		public sealed class UintEvent : UnityEvent<uint>
		{
		}
	}
}
