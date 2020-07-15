// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using UnityEngine;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.Helpers;

namespace Zor.SimpleBlackboard.Serialization
{
	/// <summary>
	/// <para>Serialized table that has set keys and struct values.</para>
	/// <para>Inherit this class to get this functionality.</para>
	/// </summary>
	/// <typeparam name="T">Struct type of the value.</typeparam>
	public abstract class StructSerializedValueSerializedTable<T> : SerializedValueSerializedTable_Base where T : struct
	{
#pragma warning disable CS0649
		[SerializeField] private string[] m_Keys;
		[SerializeField] private T[] m_Values;
#pragma warning restore CS0649

		/// <inheritdoc/>
		public sealed override Type valueType => typeof(T);

		/// <inheritdoc/>
		public sealed override void Apply(Blackboard blackboard)
		{
			for (int i = 0, count = Mathf.Min(m_Keys.Length, m_Values.Length); i < count; ++i)
			{
				blackboard.SetStructValue(new BlackboardPropertyName(m_Keys[i]), m_Values[i]);
			}
		}

		/// <inheritdoc/>
		public sealed override void GetKeys(List<(string, Type)> keys)
		{
			int count = m_Keys.Length;
			ListHelper.EnsureCapacity(keys, count);

			for (int i = 0; i < count; ++i)
			{
				keys.Add((m_Keys[i], valueType));
			}
		}
	}
}
