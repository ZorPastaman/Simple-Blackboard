// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	/// <summary>
	/// <para>Accessor for a property of type <typeparamref name="T"/> in a blackboard.</para>
	/// <para>Inherit this to implement a new type support.</para>
	/// <para>It's recommended to inherit
	/// <see cref="ClassAccessor{T,TEvent}"/> or <see cref="StructAccessor{T,TEvent}"/>.</para>
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <typeparam name="TEvent">Unity event which is invoked on <see cref="Flush"/>.</typeparam>
	public abstract class Accessor<T, TEvent> : Accessor_Base where TEvent : UnityEvent<T>
	{
#pragma warning disable CS0649
		[SerializeField] private TEvent m_OnFlushed;
#pragma warning restore CS0649

		/// <summary>
		/// Unity event that is invoked on <see cref="Flush"/>.
		/// </summary>
		[NotNull]
		public TEvent onFlushed
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_OnFlushed;
		}

		[Preserve]
		public abstract T value { get; set; }

		/// <summary>
		/// Invokes a Unity Event with a current value.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Preserve]
		public sealed override void Flush()
		{
			m_OnFlushed.Invoke(value);
		}
	}
}
