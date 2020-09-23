// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	public abstract class StructAccessor<T, TEvent> : Accessor<T, TEvent> where T : struct where TEvent : UnityEvent<T>
	{
		[Preserve]
		public override T value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get
			{
				m_BlackboardPropertyReference.blackboardContainer.blackboard.TryGetStructValue(propertyName,
					out T answer);
				return answer;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_BlackboardPropertyReference.blackboardContainer.blackboard.SetStructValue(propertyName, value);
		}
	}
}
