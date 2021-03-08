// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// This flusher calls <see cref="AccessorFlusher.FlushAccessors"/>
	/// every time in <see cref="Coroutine"/> after <see cref="WaitForFixedUpdate"/>.
	/// </summary>
	/// <seealso cref="WaitForNullAccessorFlusher"/>
	/// <seealso cref="WaitForSecondsAccessorFlusher"/>
	[AddComponentMenu(AddComponentConstants.AccessorFlushersFolder + "Wait For Fixed Update Accessor Flusher")]
	public sealed class WaitForFixedUpdateAccessorFlusher : CoroutineAccessorFlusher
	{
		[NotNull]
		protected override YieldInstruction instruction
		{
			[Pure]
			get => new WaitForFixedUpdate();
		}
	}
}
