// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// This flusher calls <see cref="AccessorFlusher.FlushAccessors"/>
	/// every <see cref="Update"/>.
	/// </summary>
	/// <seealso cref="FixedUpdateAccessorFlusher"/>
	/// <seealso cref="LateUpdateAccessorFlusher"/>
	[AddComponentMenu(AddComponentConstants.AccessorFlushersFolder + "Update Accessor Flusher")]
	public sealed class UpdateAccessorFlusher : AccessorFlusher
	{
		private void Update()
		{
			FlushAccessors();
		}
	}
}
