// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Quaternion Accessor")]
	public sealed class QuaternionAccessor : StructAccessor<Quaternion, QuaternionAccessor.QuaternionEvent>
	{
		[Serializable]
		public sealed class QuaternionEvent : UnityEvent<Quaternion>
		{
		}
	}
}
