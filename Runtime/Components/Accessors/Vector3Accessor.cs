// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Vector3 Accessor")]
	public sealed class Vector3Accessor : StructAccessor<Vector3, Vector3Accessor.Vector3Event>
	{
		[Serializable]
		public sealed class Vector3Event : UnityEvent<Vector3>
		{
		}
	}
}
