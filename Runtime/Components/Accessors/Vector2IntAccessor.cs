// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Vector2 Int Accessor")]
	public sealed class Vector2IntAccessor : StructAccessor<Vector2Int, Vector2IntAccessor.Vector2IntEvent>
	{
		[Serializable]
		public sealed class Vector2IntEvent : UnityEvent<Vector2Int>
		{
		}
	}
}
