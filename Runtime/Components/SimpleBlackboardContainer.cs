// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.Helpers;
using Zor.SimpleBlackboard.Serialization;

namespace Zor.SimpleBlackboard.Components
{
	/// <summary>
	/// Container of <see cref="Zor.SimpleBlackboard.Core.Blackboard"/> for using that as
	/// <see cref="UnityEngine.Component"/>.
	/// </summary>
	[AddComponentMenu(AddComponentConstants.SimpleBlackboardFolder + "Simple Blackboard Container")]
	public sealed class SimpleBlackboardContainer : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField,
		Tooltip("Array of serialized properties for Blackboard.\nIt is automatically applied to Blackboard on Awake.")]
		private SimpleSerializedContainer[] m_SerializedContainers;
		[SerializeField,
		Tooltip("Array of serialized references to local components for Blackboard.\nIt is automatically applied to Blackboard on Awake.")]
		private ComponentReference[] m_ComponentReferences;
#pragma warning restore CS0649

		private Blackboard m_blackboard;

		/// <summary>
		/// Contained <see cref="Blackboard"/>.
		/// </summary>
		/// <remarks>
		/// It's not recommended to cache this value.
		/// </remarks>
		[NotNull]
		public Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

		/// <summary>
		/// How many serialized containers this <see cref="SimpleBlackboardContainer"/> depends on.
		/// </summary>
		public int serializedContainersCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_SerializedContainers.Length;
		}

		/// <summary>
		/// How many component references this <see cref="SimpleBlackboardContainer"/> depends on.
		/// </summary>
		public int componentReferencesCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_ComponentReferences.Length;
		}

		/// <summary>
		/// Gets a <see cref="SimpleSerializedContainer"/> at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="index"></param>
		/// <returns><see cref="SimpleSerializedContainer"/> at the index <paramref name="index"/>.</returns>
		/// <remarks>
		/// If you change a gotten <see cref="SetSerializedContainer"/>,
		/// you need to call <see cref="RecreateBlackboard"/> to apply changes.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull, Pure]
		public SimpleSerializedContainer GetSerializedContainer(int index)
		{
			return m_SerializedContainers[index];
		}

		/// <summary>
		/// Sets the serialized container <paramref name="serializedContainer"/> at the index <paramref name="index"/>
		/// </summary>
		/// <param name="serializedContainer"></param>
		/// <param name="index"></param>
		/// <remarks>
		/// You need to call <see cref="RecreateBlackboard"/> to apply changes.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSerializedContainer([NotNull] SimpleSerializedContainer serializedContainer, int index)
		{
			m_SerializedContainers[index] = serializedContainer;
		}

		/// <summary>
		/// Sets the serialized containers <paramref name="serializedContainers"/>.
		/// </summary>
		/// <param name="serializedContainers"></param>
		/// <remarks>
		/// You need to call <see cref="RecreateBlackboard"/> to apply changes.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSerializedContainers([NotNull] SimpleSerializedContainer[] serializedContainers)
		{
			m_SerializedContainers = serializedContainers;
		}

		/// <summary>
		/// Gets a <see cref="ComponentReference"/> at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="index"></param>
		/// <returns><see cref="ComponentReference"/> at the index <paramref name="index"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public ComponentReference GetComponentReference(int index)
		{
			return m_ComponentReferences[index];
		}

		/// <summary>
		/// Sets the component reference <paramref name="componentReference"/> at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="componentReference"></param>
		/// <param name="index"></param>
		/// <remarks>
		/// You need to call <see cref="RecreateBlackboard"/> to apply changes.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetComponentReference(ComponentReference componentReference, int index)
		{
			m_ComponentReferences[index] = componentReference;
		}

		/// <summary>
		/// Sets the component references <paramref name="componentReferences"/>.
		/// </summary>
		/// <param name="componentReferences"></param>
		/// <remarks>
		/// You need to call <see cref="RecreateBlackboard"/> to apply changes.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetComponentReferences(ComponentReference[] componentReferences)
		{
			m_ComponentReferences = componentReferences;
		}

		/// <summary>
		/// Creates a new <see cref="Blackboard"/> and applies current serialized containers to it.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining), ContextMenu("Recreate Blackboard")]
		public void RecreateBlackboard()
		{
			Awake();
		}

		private void Awake()
		{
			var newBlackboard = new Blackboard();

#if SIMPLE_BLACKBOARD_MULTITHREADING
			lock (newBlackboard)
#endif
			{
				m_blackboard = newBlackboard;
				DeserializationHelper.Deserialize(m_SerializedContainers, m_blackboard);
				DeserializationHelper.Deserialize(m_ComponentReferences, m_blackboard);
			}
		}

		[ContextMenu("Log")]
		private void Log()
		{
			Debug.Log(m_blackboard.ToString(), this);
		}
	}
}
