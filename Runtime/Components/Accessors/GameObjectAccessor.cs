// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "GameObject Accessor")]
	public sealed class GameObjectAccessor : ClassAccessor<GameObject, GameObjectAccessor.GameObjectEvent>
	{
		[Serializable]
		public sealed class GameObjectEvent : UnityEvent<GameObject>
		{
		}
	}
}
