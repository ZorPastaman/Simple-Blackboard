// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Color Accessor")]
	public sealed class ColorAccessor : StructAccessor<Color, ColorAccessor.ColorEvent>
	{
		[Serializable]
		public sealed class ColorEvent : UnityEvent<Color>
		{
		}
	}
}
