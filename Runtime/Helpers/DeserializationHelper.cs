// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Components;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.Debugging;
using Zor.SimpleBlackboard.Serialization;

namespace Zor.SimpleBlackboard.Helpers
{
	/// <summary>
	/// A collection of static methods that allow to easily apply a <see cref="SimpleSerializedContainer"/>
	/// to a <see cref="Blackboard"/>.
	/// </summary>
	public static class DeserializationHelper
	{
		/// <summary>
		/// Creates a new <see cref="Blackboard"/>, applies <paramref name="serializedContainer"/> to it
		/// and returns it.
		/// </summary>
		/// <param name="serializedContainer"></param>
		/// <returns>New <see cref="Blackboard"/> with applied <paramref name="serializedContainer"/>.</returns>
		public static Blackboard Deserialize([NotNull] SimpleSerializedContainer serializedContainer)
		{
			return Deserialize(serializedContainer, new Blackboard());
		}

		/// <summary>
		/// Applies <paramref name="serializedContainer"/> to <paramref name="blackboard"/> and returns it.
		/// </summary>
		/// <param name="serializedContainer"></param>
		/// <param name="blackboard"></param>
		/// <returns><paramref name="blackboard"/> after applying <paramref name="serializedContainer"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Blackboard Deserialize([NotNull] SimpleSerializedContainer serializedContainer,
			[NotNull] Blackboard blackboard)
		{
			serializedContainer.Apply(blackboard);
			return blackboard;
		}

		/// <summary>
		/// Creates a new <see cref="Blackboard"/>, applies <paramref name="serializedContainers"/> to it
		/// and returns it.
		/// </summary>
		/// <param name="serializedContainers"></param>
		/// <returns>New <see cref="Blackboard"/> with applied <paramref name="serializedContainers"/>.</returns>
		public static Blackboard Deserialize([NotNull] SimpleSerializedContainer[] serializedContainers)
		{
			return Deserialize(serializedContainers, new Blackboard());
		}

		/// <summary>
		/// Applies <paramref name="serializedContainers"/> to <paramref name="blackboard"/> and return it.
		/// </summary>
		/// <param name="serializedContainers"></param>
		/// <param name="blackboard"></param>
		/// <returns><paramref name="blackboard"/> after applying <paramref name="serializedContainers"/>.</returns>
		public static Blackboard Deserialize([NotNull] SimpleSerializedContainer[] serializedContainers,
			[NotNull] Blackboard blackboard)
		{
			for (int i = 0, count = serializedContainers.Length; i < count; ++i)
			{
				SimpleSerializedContainer container = serializedContainers[i];

				if (container == null)
				{
					BlackboardDebug.LogWarning($"[BlackboardContainer] SerializedContainer at index '{i}' is null");
					continue;
				}

				container.Apply(blackboard);
			}

			return blackboard;
		}

		/// <summary>
		/// Creates a new <see cref="Blackboard"/>, applies <paramref name="componentReference"/> to it
		/// and returns it.
		/// </summary>
		/// <param name="componentReference"></param>
		/// <returns>New <see cref="Blackboard"/> with applied <paramref name="componentReference"/>.</returns>
		public static Blackboard Deserialize(ComponentReference componentReference)
		{
			return Deserialize(componentReference, new Blackboard());
		}

		/// <summary>
		/// Applies <paramref name="componentReference"/> to <paramref name="blackboard"/> and returns it.
		/// </summary>
		/// <param name="componentReference"></param>
		/// <param name="blackboard"></param>
		/// <returns><paramref name="blackboard"/> after applying <paramref name="componentReference"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Blackboard Deserialize(ComponentReference componentReference, [NotNull] Blackboard blackboard)
		{
			componentReference.Apply(blackboard);
			return blackboard;
		}

		/// <summary>
		/// Creates a new <see cref="Blackboard"/>, applies <paramref name="componentReferences"/> to it
		/// and returns it.
		/// </summary>
		/// <param name="componentReferences"></param>
		/// <returns>New <see cref="Blackboard"/> with applied <paramref name="componentReferences"/>.</returns>
		public static Blackboard Deserialize([NotNull] ComponentReference[] componentReferences)
		{
			return Deserialize(componentReferences, new Blackboard());
		}

		/// <summary>
		/// Applies <paramref name="componentReferences"/> to <paramref name="blackboard"/> and returns it.
		/// </summary>
		/// <param name="componentReferences"></param>
		/// <param name="blackboard"></param>
		/// <returns><paramref name="blackboard"/> after applying <paramref name="componentReferences"/>.</returns>
		public static Blackboard Deserialize([NotNull] ComponentReference[] componentReferences,
			[NotNull] Blackboard blackboard)
		{
			for (int i = 0, count = componentReferences.Length; i < count; ++i)
			{
				componentReferences[i].Apply(blackboard);
			}

			return blackboard;
		}
	}
}
