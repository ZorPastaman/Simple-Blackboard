// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.SimpleBlackboard.Components.Accessors;

namespace Zor.SimpleBlackboard.Components.AccessorFlushers
{
	/// <summary>
	/// Inherit this component to implement your Flusher for <see cref="Accessor_Base.Flush"/>.
	/// </summary>
	/// <remarks>
	/// You need to call <see cref="FlushAccessors"/> for flushing.
	/// </remarks>
	public abstract class AccessorFlusher : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField] private Accessor_Base[] m_Accessors;
#pragma warning restore CS0649

		/// <summary>
		/// How many accessors this <see cref="AccessorFlusher"/> depends on.
		/// </summary>
		public int accessorsCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_Accessors.Length;
		}

		/// <summary>
		/// Gets a <see cref="Accessor_Base"/> at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="index"></param>
		/// <returns><see cref="Accessor_Base"/> at the index <paramref name="index"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull, Pure]
		public Accessor_Base GetAccessor(int index)
		{
			return m_Accessors[index];
		}

		/// <summary>
		/// Sets the accessor <paramref name="accessor"/> at the index <paramref name="index"/>
		/// </summary>
		/// <param name="accessor"></param>
		/// <param name="index"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetAccessor([NotNull] Accessor_Base accessor, int index)
		{
			m_Accessors[index] = accessor;
		}

		/// <summary>
		/// Sets the accessors <paramref name="accessors"/>.
		/// </summary>
		/// <param name="accessors"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetAccessors([NotNull] Accessor_Base[] accessors)
		{
			m_Accessors = accessors;
		}

		/// <summary>
		/// Call this method for flushing.
		/// </summary>
		protected void FlushAccessors()
		{
			for (int i = 0, count = m_Accessors.Length; i < count; ++i)
			{
				m_Accessors[i].Flush();
			}
		}

		protected virtual void OnValidate()
		{
		}
	}
}
