// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Char Accessor")]
	public sealed class CharAccessor : StructAccessor<char, CharAccessor.CharEvent>
	{
		[Serializable]
		public sealed class CharEvent : UnityEvent<char>
		{
		}
	}
}
