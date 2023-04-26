// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Sprite Accessor")]
	public sealed class SpriteAccessor : ClassAccessor<Sprite, SpriteAccessor.SpriteEvent>
	{
		[Serializable]
		public sealed class SpriteEvent : UnityEvent<Sprite>
		{
		}
	}
}
