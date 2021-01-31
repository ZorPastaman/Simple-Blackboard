// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// Inherit this component to implement your Flusher for <see cref="AccessorFlusher.FlushAccessors"/>
	/// based on <see cref="UnityEngine.Coroutine"/>.
	/// </summary>
	public abstract class CoroutineAccessorFlusher : AccessorFlusher
	{
		private YieldInstruction m_instruction;
		private Coroutine m_coroutine;

		/// <summary>
		/// <see cref="UnityEngine.YieldInstruction"/> which is processed before calling
		/// <see cref="Zor.SimpleBlackboard.Components.AccessorFlushers.AccessorFlusher.FlushAccessors"/>.
		/// </summary>
		protected abstract YieldInstruction instruction { get; }

		/// <summary>
		/// Gets a new instruction from <see cref="instruction"/>.
		/// </summary>
		/// <remarks>
		/// It doesn't recreate a current coroutine.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining), ContextMenu("Update Instruction")]
		protected void UpdateInstruction()
		{
			m_instruction = instruction;
		}

		protected override void OnValidate()
		{
			m_instruction = instruction;
		}

		private void Awake()
		{
			m_instruction = instruction;
		}

		private void OnEnable()
		{
			m_coroutine = StartCoroutine(Process());
		}

		private void OnDisable()
		{
			if (m_coroutine != null)
			{
				StopCoroutine(m_coroutine);
				m_coroutine = null;
			}
		}

		private IEnumerator Process()
		{
			while (true)
			{
				yield return m_instruction;
				FlushAccessors();
			}
		}
	}
}
