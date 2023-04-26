// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.SimpleBlackboard.Components
{
	/// <summary>
	/// Useful serializable struct for referencing a property in <see cref="Zor.SimpleBlackboard.Core.Blackboard"/>
	/// in <see cref="SimpleBlackboardContainer"/>. It has a special editor.
	/// </summary>
	/// <remarks>
	/// Usually it's used in <see cref="UnityEngine.Component"/>.
	/// </remarks>
	[Serializable]
	public struct BlackboardPropertyReference : IEquatable<BlackboardPropertyReference>
	{
#pragma warning disable CS0649
		[SerializeField, NotNull] private SimpleBlackboardContainer m_BlackboardContainer;
		[SerializeField, NotNull] private string m_PropertyName;
#pragma warning restore CS0649

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BlackboardPropertyReference([NotNull] SimpleBlackboardContainer blackboardContainer,
			[NotNull] string propertyName)
		{
			m_BlackboardContainer = blackboardContainer;
			m_PropertyName = propertyName;
		}

		[NotNull]
		public SimpleBlackboardContainer blackboardContainer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_BlackboardContainer;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_BlackboardContainer = value;
		}

		[NotNull]
		public string propertyName
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_PropertyName;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_PropertyName = value;
		}

		[Pure]
		public bool Equals(BlackboardPropertyReference other)
		{
			return Equals(m_BlackboardContainer, other.m_BlackboardContainer) &&
				string.Equals(m_PropertyName, other.m_PropertyName, StringComparison.InvariantCulture);
		}

		[Pure]
		public override bool Equals(object obj)
		{
			return obj is BlackboardPropertyReference other && Equals(other);
		}

		[Pure]
		public override int GetHashCode()
		{
			unchecked
			{
				return ((m_BlackboardContainer != null ? m_BlackboardContainer.GetHashCode() : 0) * 397) ^
					(m_PropertyName != null ? StringComparer.InvariantCulture.GetHashCode(m_PropertyName) : 0);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static bool operator ==(BlackboardPropertyReference left, BlackboardPropertyReference right)
		{
			return left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static bool operator !=(BlackboardPropertyReference left, BlackboardPropertyReference right)
		{
			return !left.Equals(right);
		}
	}
}
