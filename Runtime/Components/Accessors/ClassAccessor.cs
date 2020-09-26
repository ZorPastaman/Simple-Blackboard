// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace Zor.SimpleBlackboard.Components.Accessors
{
	/// <summary>
	/// <para>Accessor for a property of class type <typeparamref name="T"/> in a blackboard.</para>
	/// <para>Inherit this to implement a new type support.</para>
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <typeparam name="TEvent">Unity event which is invoked on <see cref="Accessor{T,TEvent}.Flush"/>.</typeparam>
	public abstract class ClassAccessor<T, TEvent> : Accessor<T, TEvent> where T : class where TEvent : UnityEvent<T>
	{
		[Preserve]
		public override T value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get
			{
				blackboardPropertyReference.blackboardContainer.blackboard.TryGetClassValue(propertyName, out T answer);
				return answer;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => blackboardPropertyReference.blackboardContainer.blackboard.SetClassValue(propertyName, value);
		}
	}
}
