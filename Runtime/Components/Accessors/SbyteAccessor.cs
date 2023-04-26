// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Sbyte Accessor")]
	public sealed class SbyteAccessor : StructAccessor<sbyte, SbyteAccessor.SbyteEvent>
	{
		[Serializable]
		public sealed class SbyteEvent : UnityEvent<sbyte>
		{
		}
	}
}
