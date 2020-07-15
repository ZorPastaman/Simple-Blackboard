// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	/// <summary>
	/// <para>Accessor for a property of type <typeparamref name="T"/> in a blackboard.</para>
	/// <para>Inherit this to implement a new type support.</para>
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <typeparam name="TEvent">Unity event which is invoked on <see cref="Flush"/>.</typeparam>
	public abstract class Accessor<T, TEvent> : Accessor_Base where TEvent : UnityEvent<T>
	{
#pragma warning disable CS0649
		[SerializeField] private TEvent m_OnFlushed;
#pragma warning restore CS0649

		[Preserve]
		public abstract T value { get; set; }

		/// <summary>
		/// Invokes a Unity Event with a current value.
		/// </summary>
		[Preserve]
		public override void Flush()
		{
			m_OnFlushed.Invoke(value);
		}
	}
}
