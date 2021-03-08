// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// This flusher calls <see cref="AccessorFlusher.FlushAccessors"/>
	/// every <see cref="LateUpdate"/>.
	/// </summary>
	/// <seealso cref="FixedUpdateAccessorFlusher"/>
	/// <seealso cref="UpdateAccessorFlusher"/>
	[AddComponentMenu(AddComponentConstants.AccessorFlushersFolder + "Late Update Accessor Flusher")]
	public sealed class LateUpdateAccessorFlusher : AccessorFlusher
	{
		private void LateUpdate()
		{
			FlushAccessors();
		}
	}
}
