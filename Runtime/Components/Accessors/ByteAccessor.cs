// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Byte Accessor")]
	public sealed class ByteAccessor : StructAccessor<byte, ByteAccessor.ByteEvent>
	{
		[Serializable]
		public sealed class ByteEvent : UnityEvent<byte>
		{
		}
	}
}
