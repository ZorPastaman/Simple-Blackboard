// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// This flusher calls <see cref="AccessorFlusher.FlushAccessors"/>
	/// every time in <see cref="Coroutine"/> after null.
	/// </summary>
	/// <seealso cref="WaitForFixedUpdateAccessorFlusher"/>
	/// <seealso cref="WaitForSecondsAccessorFlusher"/>
	[AddComponentMenu(AddComponentConstants.AccessorFlushersFolder + "Wait For Null Accessor Flusher")]
	public sealed class WaitForNullAccessorFlusher : CoroutineAccessorFlusher
	{
		protected override YieldInstruction instruction
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => null;
		}
	}
}
