// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "String Accessor")]
	public sealed class StringAccessor : ClassAccessor<string, StringAccessor.StringEvent>
	{
		[Serializable]
		public sealed class StringEvent : UnityEvent<string>
		{
		}
	}
}
