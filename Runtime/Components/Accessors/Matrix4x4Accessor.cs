// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Matrix4x4 Accessor")]
	public sealed class Matrix4x4Accessor : StructAccessor<Matrix4x4, Matrix4x4Accessor.Matrix4x4Event>
	{
		[Serializable]
		public sealed class Matrix4x4Event : UnityEvent<Matrix4x4>
		{
		}
	}
}
