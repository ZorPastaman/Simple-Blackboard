// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	/// <summary>
	/// <para>Base class for accessors.</para>
	/// <para>It's recommended to inherit <see cref="Accessor{T,TEvent}"/>.</para>
	/// </summary>
	public abstract class Accessor_Base : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField] private BlackboardPropertyReference m_BlackboardPropertyReference;
#pragma warning restore CS0649

		private BlackboardPropertyName m_blackboardPropertyName;
		private bool m_initialized;

		public BlackboardPropertyReference blackboardPropertyReference
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_BlackboardPropertyReference;
			set
			{
				if (m_BlackboardPropertyReference == value)
				{
					return;
				}

				m_BlackboardPropertyReference = value;
				m_initialized = false;
			}
		}

		protected BlackboardPropertyName propertyName
		{
			get
			{
				if (!m_initialized)
				{
					m_blackboardPropertyName =
						new BlackboardPropertyName(m_BlackboardPropertyReference.propertyName);
					m_initialized = true;
				}

				return m_blackboardPropertyName;
			}
		}

		[Preserve]
		public abstract void Flush();
	}
}
