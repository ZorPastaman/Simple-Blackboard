// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Object Accessor")]
	public sealed class ObjectAccessor : ClassAccessor<Object, ObjectAccessor.ObjectEvent>
	{
		[Serializable]
		public sealed class ObjectEvent : UnityEvent<Object>
		{
		}
	}
}
