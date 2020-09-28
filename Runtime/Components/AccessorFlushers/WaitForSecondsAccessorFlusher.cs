// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// This flusher calls <see cref="AccessorFlusher.FlushAccessors"/>
	/// every time in <see cref="Coroutine"/> after <see cref="WaitForSeconds"/>.
	/// </summary>
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

		protected override YieldInstruction instruction => new WaitForSeconds(m_Seconds);
	}
}
