// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Component Accessor")]
	public sealed class ComponentAccessor : ClassAccessor<Component, ComponentAccessor.ComponentEvent>
	{
		[Serializable]
		public sealed class ComponentEvent : UnityEvent<Component>
		{
		}
	}
}
