// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
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
		/// <summary>
		/// Property names to values dictionary.
		/// </summary>
		private readonly Dictionary<BlackboardPropertyName, T> m_table = new Dictionary<BlackboardPropertyName, T>();

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
			get => m_table.Count;
		}

		/// <summary>
		/// Gets a value of the property name <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to get.</param>
		/// <returns>Gotten value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), CanBeNull, Pure]
		public T GetValue(BlackboardPropertyName propertyName)
		{
			return m_table[propertyName];
		}

		/// <inheritdoc/>
		public object GetObjectValue(BlackboardPropertyName propertyName)
		{
			return m_table[propertyName];
		}

		/// <summary>
		/// Sets the value <paramref name="value"/> of the property name <paramref name="propertyName"/>
		/// </summary>
		/// <param name="propertyName">Name of the property to set.</param>
		/// <param name="value">Set value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetValue(BlackboardPropertyName propertyName, [CanBeNull] T value)
		{
			m_table[propertyName] = value;
		}

		/// <inheritdoc/>
		public void SetObjectValue(BlackboardPropertyName propertyName, object value)
		{
			m_table[propertyName] = value is T typedValue
				? typedValue
				: default;
		}

		/// <summary>
		/// Gets all properties and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Properties are added to this.</param>
		/// <seealso cref="GetProperties(System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,object}})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetProperties([NotNull] List<KeyValuePair<BlackboardPropertyName, T>> properties)
		{
			properties.AddRange(m_table);
		}

		/// <inheritdoc/>
		/// <seealso cref="GetProperties(System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,T}})"/>
		public void GetProperties(List<KeyValuePair<BlackboardPropertyName, object>> properties)
		{
			Dictionary<BlackboardPropertyName, T>.Enumerator enumerator = m_table.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<BlackboardPropertyName, T> current = enumerator.Current;
				properties.Add(new KeyValuePair<BlackboardPropertyName, object>(current.Key, current.Value));
			}
			enumerator.Dispose();
		}

		/// <inheritdoc/>
		public void GetPropertiesAs<TAs>(List<KeyValuePair<BlackboardPropertyName, TAs>> properties)
			where TAs : class
		{
			Dictionary<BlackboardPropertyName, T>.Enumerator enumerator = m_table.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<BlackboardPropertyName, T> current = enumerator.Current;
				properties.Add(new KeyValuePair<BlackboardPropertyName, TAs>(current.Key, current.Value as TAs));
			}
			enumerator.Dispose();
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Remove(BlackboardPropertyName propertyName)
		{
			return m_table.Remove(propertyName);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			m_table.Clear();
		}

		/// <inheritdoc/>
		public void CopyTo(IBlackboardTable table)
		{
			var typedTable = (BlackboardTable<T>)table;

			Dictionary<BlackboardPropertyName, T>.Enumerator enumerator = m_table.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<BlackboardPropertyName, T> current = enumerator.Current;
				typedTable.SetValue(current.Key, current.Value);
			}
			enumerator.Dispose();
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			Dictionary<BlackboardPropertyName, T>.Enumerator enumerator = m_table.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<BlackboardPropertyName, T> current = enumerator.Current;
				builder.Append($"[{current.Key.ToString()}, {current.Value.ToString()}], ");
			}
			enumerator.Dispose();

			int builderLength = builder.Length;
			int length = builderLength >= 2 ? builderLength - 2 : 0;

			return builder.ToString(0, length);
		}
	}
}
