// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

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
		[SerializeField] protected BlackboardPropertyReference m_BlackboardPropertyReference;
#pragma warning restore CS0649

		private BlackboardPropertyName m_blackboardPropertyName;
		private bool m_initialized;

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
