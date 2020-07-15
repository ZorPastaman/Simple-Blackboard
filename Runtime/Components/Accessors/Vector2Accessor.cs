// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Vector2 Accessor")]
	public sealed class Vector2Accessor : StructAccessor<Vector2, Vector2Accessor.Vector2Event>
	{
		[Serializable]
		public sealed class Vector2Event : UnityEvent<Vector2>
		{
		}
	}
}
