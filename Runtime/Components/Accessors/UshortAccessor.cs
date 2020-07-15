// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Ushort Accessor")]
	public sealed class UshortAccessor : StructAccessor<ushort, UshortAccessor.UshortEvent>
	{
		[Serializable]
		public sealed class UshortEvent : UnityEvent<ushort>
		{
		}
	}
}
