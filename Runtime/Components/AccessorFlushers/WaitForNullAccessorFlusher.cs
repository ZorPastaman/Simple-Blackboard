// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// This flusher calls <see cref="AccessorFlusher.FlushAccessors"/>
	/// every time in <see cref="Coroutine"/> after null.
	/// </summary>
	[AddComponentMenu(AddComponentConstants.AccessorFlushersFolder + "Wait For Null Accessor Flusher")]
	public sealed class WaitForNullAccessorFlusher : CoroutineAccessorFlusher
	{
		protected override YieldInstruction instruction => null;
	}
}
