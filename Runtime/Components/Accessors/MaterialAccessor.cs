// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Material Accessor")]
	public sealed class MaterialAccessor : ClassAccessor<Material, MaterialAccessor.MaterialEvent>
	{
		[Serializable]
		public sealed class MaterialEvent : UnityEvent<Material>
		{
		}
	}
}
