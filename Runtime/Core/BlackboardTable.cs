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
	internal sealed class BlackboardTable<T> : Dictionary<BlackboardPropertyName, T>, IBlackboardTable
	{
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
			get => Count;
		}

		/// <summary>
		/// Gets a value of the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to get.</param>
		/// <returns>Gotten value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), CanBeNull, Pure]
		public T GetValue(BlackboardPropertyName propertyName)
		{
			return this[propertyName];
		}

		/// <inheritdoc/>
		[Pure]
		public object GetObjectValue(BlackboardPropertyName propertyName)
		{
			return this[propertyName];
		}

		/// <summary>
		/// Sets the <paramref name="value"/> of the <paramref name="propertyName"/>
		/// </summary>
		/// <param name="propertyName">Name of the property to set.</param>
		/// <param name="value">Set value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetValue(BlackboardPropertyName propertyName, [CanBeNull] T value)
		{
			this[propertyName] = value;
		}

		/// <inheritdoc/>
		public void SetObjectValue(BlackboardPropertyName propertyName, object value)
		{
			this[propertyName] = value is T typedValue
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
			properties.AddRange(this);
		}

		/// <inheritdoc/>
		/// <seealso cref="GetProperties(System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,T}})"/>
		public void GetProperties(List<KeyValuePair<BlackboardPropertyName, object>> properties)
		{
			Enumerator enumerator = GetEnumerator();
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
			Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<BlackboardPropertyName, T> current = enumerator.Current;
				properties.Add(new KeyValuePair<BlackboardPropertyName, TAs>(current.Key, current.Value as TAs));
			}
			enumerator.Dispose();
		}

		/// <inheritdoc/>
		public void CopyTo(IBlackboardTable table)
		{
			var typedTable = (BlackboardTable<T>)table;

			Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<BlackboardPropertyName, T> current = enumerator.Current;
				typedTable.SetValue(current.Key, current.Value);
			}
			enumerator.Dispose();
		}

		[Pure]
		public override string ToString()
		{
			var builder = new StringBuilder();

			Enumerator enumerator = GetEnumerator();
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
