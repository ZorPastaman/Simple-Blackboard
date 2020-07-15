// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Float Accessor")]
	public sealed class FloatAccessor : StructAccessor<float, FloatAccessor.FloatEvent>
	{
		[Serializable]
		public sealed class FloatEvent : UnityEvent<float>
		{
		}
	}
}
