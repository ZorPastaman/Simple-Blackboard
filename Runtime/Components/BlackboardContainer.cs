// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.Debugging;
using Zor.SimpleBlackboard.Serialization;

namespace Zor.SimpleBlackboard.Components
{
	/// <summary>
	/// Container of <see cref="Zor.SimpleBlackboard.Core.Blackboard"/> for using that as
	/// <see cref="UnityEngine.Component"/>.
	/// </summary>
	[AddComponentMenu(AddComponentConstants.SimpleBlackboardFolder + "Blackboard Container")]
	public sealed class BlackboardContainer : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField, Tooltip("Array of serialized properties for Blackboard.\nIt is applied to Blackboard only on Awake.")]
		private SerializedContainer[] m_SerializedContainers;
#pragma warning restore CS0649

		private Blackboard m_blackboard;

		public Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

		private void Awake()
		{
			m_blackboard = new Blackboard();

			for (int i = 0, count = m_SerializedContainers.Length; i < count; ++i)
			{
				SerializedContainer container = m_SerializedContainers[i];

				if (container == null)
				{
					BlackboardDebug.LogWarning($"[BlackboardContainer] SerializedContainer at index '{i}' is null", this);
					continue;
				}

				container.Apply(m_blackboard);
			}
		}

		[ContextMenu("Log")]
		private void Log()
		{
			UnityEngine.Debug.Log(m_blackboard.ToString(), this);
		}
	}
}
