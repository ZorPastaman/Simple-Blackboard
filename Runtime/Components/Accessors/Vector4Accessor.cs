// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Vector4 Accessor")]
	public sealed class Vector4Accessor : StructAccessor<Vector4, Vector4Accessor.Vector4Event>
	{
		[Serializable]
		public sealed class Vector4Event : UnityEvent<Vector4>
		{
		}
	}
}
