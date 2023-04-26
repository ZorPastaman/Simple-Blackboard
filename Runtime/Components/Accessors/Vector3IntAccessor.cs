// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Vector3 Int Accessor")]
	public sealed class Vector3IntAccessor : StructAccessor<Vector3Int, Vector3IntAccessor.Vector3IntEvent>
	{
		[Serializable]
		public sealed class Vector3IntEvent : UnityEvent<Vector3Int>
		{
		}
	}
}
