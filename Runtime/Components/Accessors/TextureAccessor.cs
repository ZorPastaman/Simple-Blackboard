// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	[AddComponentMenu(AddComponentConstants.AccessorsFolder + "Texture Accessor")]
	public sealed class TextureAccessor : ClassAccessor<Texture, TextureAccessor.TextureEvent>
	{
		[Serializable]
		public sealed class TextureEvent : UnityEvent<Texture>
		{
		}
	}
}
