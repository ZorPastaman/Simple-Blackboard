// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.Components
{
	/// <summary>
	/// Pair of a property name and a <see cref="Component"/> reference.
	/// It's used in <see cref="SimpleBlackboardContainer"/> to save references to a local component.
	/// </summary>
	[Serializable]
	public struct ComponentReference : IEquatable<ComponentReference>
	{
#pragma warning disable CS0649
		[SerializeField, NotNull] private string m_PropertyName;
		[SerializeField, CanBeNull] private Component m_Component;
#pragma warning restore CS0649

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ComponentReference([NotNull] string propertyName, [CanBeNull] Component component)
		{
			m_PropertyName = propertyName;
			m_Component = component;
		}

		[NotNull]
		public string propertyName
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_PropertyName;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_PropertyName = value;
		}

		[CanBeNull]
		public Component component
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_Component;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Component = value;
		}

		/// <summary>
		/// Applies a pair of a property name and a <see cref="Component"/> reference to <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Apply([NotNull] Blackboard blackboard)
		{
			blackboard.SetClassValue(new BlackboardPropertyName(m_PropertyName), m_Component);
		}

		[Pure]
		public bool Equals(ComponentReference other)
		{
			return m_PropertyName == other.m_PropertyName && Equals(m_Component, other.m_Component);
		}

		[Pure]
		public override bool Equals(object obj)
		{
			return obj is ComponentReference other && Equals(other);
		}

		[Pure]
		public override int GetHashCode()
		{
			unchecked
			{
				return ((m_PropertyName != null ? m_PropertyName.GetHashCode() : 0) * 397) ^
					(m_Component != null ? m_Component.GetHashCode() : 0);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static bool operator ==(ComponentReference left, ComponentReference right)
		{
			return left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static bool operator !=(ComponentReference left, ComponentReference right)
		{
			return !left.Equals(right);
		}
	}
}
