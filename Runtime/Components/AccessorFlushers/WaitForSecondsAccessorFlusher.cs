// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// This flusher calls <see cref="AccessorFlusher.FlushAccessors"/>
	/// every time in <see cref="Coroutine"/> after <see cref="WaitForSeconds"/>.
	/// </summary>
	/// <seealso cref="WaitForFixedUpdateAccessorFlusher"/>
	/// <seealso cref="WaitForNullAccessorFlusher"/>
	[AddComponentMenu(AddComponentConstants.AccessorFlushersFolder + "Wait For Seconds Accessor Flusher")]
	public sealed class WaitForSecondsAccessorFlusher : CoroutineAccessorFlusher
	{
#pragma warning disable CS0649
		[SerializeField] private float m_Seconds;
#pragma warning restore CS0649

		public float seconds
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_Seconds;
			set
			{
				if (m_Seconds == value)
				{
					return;
				}

				m_Seconds = value;
				UpdateInstruction();
			}
		}

		[NotNull]
		protected override YieldInstruction instruction
		{
			[Pure]
			get => new WaitForSeconds(m_Seconds);
		}
	}
}
