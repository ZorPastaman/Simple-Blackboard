// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Transform Accessor")]
	public sealed class TransformAccessor : ClassAccessor<Transform, TransformAccessor.TransformEvent>
	{
		[Serializable]
		public sealed class TransformEvent : UnityEvent<Transform>
		{
		}
	}
}
