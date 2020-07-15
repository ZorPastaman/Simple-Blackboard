// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

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
		/// Call this method for flushing.
		/// </summary>
		protected void FlushAccessors()
		{
			for (int i = 0, count = m_Accessors.Length; i < count; ++i)
			{
				m_Accessors[i].Flush();
			}
		}
	}
}
