// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Zor.SimpleBlackboard.Core
{
	/// <summary>
	/// Entry of <see cref="Zor.SimpleBlackboard.Core.Blackboard"/>. A <see cref="BlackboardTable{T}"/>
	/// contains values of the type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	internal sealed class BlackboardTable<T> : IBlackboardTable
	{
		private readonly List<T> m_values = new();
		private readonly Stack<int> m_freeIndices = new();

		/// <summary>
		/// Type of values that are contained in the <see cref="BlackboardTable{T}"/>.
		/// </summary>
		public Type valueType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(T);
		}

		/// <summary>
		/// How many values are contained in the <see cref="BlackboardTable{T}"/>.
		/// </summary>
		public int count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_values.Count - m_freeIndices.Count;
		}

		/// <summary>
		/// Gets a value by the <paramref name="index"/>.
		/// </summary>
		/// <param name="index">Index of the property to get.</param>
		/// <returns>Gotten value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), CanBeNull, Pure]
		public T GetValue(int index)
		{
			return m_values[index];
		}

		/// <inheritdoc/>
		[Pure]
		public object GetObjectValue(int index)
		{
			return GetValue(index);
		}

		/// <summary>
		/// Sets the <paramref name="value"/> by the <paramref name="index"/>
		/// </summary>
		/// <param name="index">Name of the property to set.</param>
		/// <param name="value">Set value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetValue([CanBeNull] T value, int index)
		{
			m_values[index] = value;
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetObjectValue(object value, int index)
		{
			SetValue((T)value, index);
		}

		/// <summary>
		/// Adds the <paramref name="value"/> to the table.
		/// </summary>
		/// <param name="value">Value to add.</param>
		/// <returns>Value index.</returns>
		public int AddValue([CanBeNull] T value)
		{
			if (m_freeIndices.TryPop(out int index))
			{
				SetValue(value, index);
				return index;
			}

			m_values.Add(value);
			return m_values.Count - 1;
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int AddObjectValue(object value)
		{
			return AddValue((T)value);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Remove(int index)
		{
			m_values[index] = default;
			m_freeIndices.Push(index);
		}

		/// <inheritdoc/>
		public void Clear()
		{
			for (int i = 0, valueCount = m_values.Count; i < valueCount; ++i)
			{
				if (!m_freeIndices.Contains(i))
				{
					Remove(i);
				}
			}
		}
	}
}
