// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.Debugging;

namespace Zor.SimpleBlackboard.Serialization
{
	/// <summary>
	/// Container of serialized properties for a <see cref="Zor.SimpleBlackboard.Core.Blackboard"/>.
	/// </summary>
	[CreateAssetMenu(menuName = "Simple Blackboard/Serialized Tables Container", fileName = "SerializedTablesContainer", order = 445)]
	public sealed class SerializedTablesContainer : SerializedContainer
	{
#pragma warning disable CS0649
		[SerializeField, HideInInspector] private SerializedTable_Base[] m_SerializedTables;
#pragma warning restore CS0649

		/// <summary>
		/// How many serialized tables are contained.
		/// </summary>
		public int serializedTablesCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_SerializedTables.Length;
		}

		/// <summary>
		/// Gets a <see cref="SerializedTable_Base"/> at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="index"></param>
		/// <returns><see cref="SerializedTable_Base"/> at the index <paramref name="index"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public SerializedTable_Base GetSerializedTable(int index)
		{
			return m_SerializedTables[index];
		}

		/// <summary>
		/// Sets the serialized table <paramref name="serializedTable"/> at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="serializedTable"></param>
		/// <param name="index"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSerializedTable([NotNull] SerializedTable_Base serializedTable, int index)
		{
			m_SerializedTables[index] = serializedTable;
		}

		/// <summary>
		/// Sets the serialized tables.
		/// </summary>
		/// <param name="serializedTables"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSerializedTables([NotNull] SerializedTable_Base[] serializedTables)
		{
			m_SerializedTables = serializedTables;
		}

		/// <inheritdoc/>
		public override void Apply(Blackboard blackboard)
		{
			for (int i = 0, count = m_SerializedTables.Length; i < count; ++i)
			{
				SerializedTable_Base table = m_SerializedTables[i];

				if (table == null)
				{
					BlackboardDebug.LogWarning($"[SerializedTablesContainer] Serialized table at index '{i}' is null");
					continue;
				}

				table.Apply(blackboard);
			}
		}

		/// <inheritdoc/>
		public override void GetKeys(List<(string, Type)> keys)
		{
			for (int i = 0, count = m_SerializedTables.Length; i < count; ++i)
			{
				SerializedTable_Base table = m_SerializedTables[i];

				if (table != null)
				{
					table.GetKeys(keys);
				}
			}
		}
	}
}
