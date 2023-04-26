// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Property Name Accessor")]
	public sealed class PropertyNameAccessor : StructAccessor<PropertyName, PropertyNameAccessor.PropertyNameEvent>
	{
		[Serializable]
		public sealed class PropertyNameEvent : UnityEvent<PropertyName>
		{
		}
	}
}
