// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Layer Mask Accessor")]
	public sealed class LayerMaskAccessor : StructAccessor<LayerMask, LayerMaskAccessor.LayerMaskEvent>
	{
		[Serializable]
		public sealed class LayerMaskEvent : UnityEvent<LayerMask>
		{
		}
	}
}
