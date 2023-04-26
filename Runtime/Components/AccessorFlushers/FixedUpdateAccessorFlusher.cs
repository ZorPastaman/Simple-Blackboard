// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// This flusher calls <see cref="AccessorFlusher.FlushAccessors"/>
	/// every <see cref="FixedUpdate"/>.
	/// </summary>
	/// <seealso cref="LateUpdateAccessorFlusher"/>
	/// <seealso cref="UpdateAccessorFlusher"/>
	[AddComponentMenu(AddComponentConstants.AccessorFlushersFolder + "Fixed Update Accessor Flusher")]
	public sealed class FixedUpdateAccessorFlusher : AccessorFlusher
	{
		private void FixedUpdate()
		{
			FlushAccessors();
		}
	}
}
