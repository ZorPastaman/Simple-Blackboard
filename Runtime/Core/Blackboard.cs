// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.SimpleBlackboard.Debugging;

namespace Zor.SimpleBlackboard.Core
{
	/// <summary>
	/// The main class of the Blackboard system.
	/// </summary>
	public sealed class Blackboard : ICollection<KeyValuePair<BlackboardPropertyName, object>>, ICollection,
		IReadOnlyCollection<KeyValuePair<BlackboardPropertyName, object>>
	{
		/// <summary>
		/// Value type to table of that type dictionary.
		/// </summary>
		[NotNull] private readonly Dictionary<Type, IBlackboardTable> m_tables = new();
		/// <summary>
		/// Property name to value index dictionary.
		/// </summary>
		[NotNull] private readonly Dictionary<BlackboardPropertyName, ValueIndex> m_properties = new();

		/// <summary>
		/// How many value types are contained in the <see cref="Blackboard"/>.
		/// </summary>
		public int valueTypesCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_tables.Count;
		}

		/// <summary>
		/// How many properties are contained in the <see cref="Blackboard"/>.
		/// </summary>
		public int propertiesCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_properties.Count;
		}

		int ICollection<KeyValuePair<BlackboardPropertyName, object>>.Count => propertiesCount;

		int ICollection.Count => propertiesCount;

		int IReadOnlyCollection<KeyValuePair<BlackboardPropertyName, object>>.Count => propertiesCount;

		bool ICollection<KeyValuePair<BlackboardPropertyName, object>>.IsReadOnly => false;

		bool ICollection.IsSynchronized => false;

		object ICollection.SyncRoot => this;

		/// <summary>
		/// Tries to get and return a value of the struct type <typeparamref name="T"/>
		/// and the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <typeparam name="T">Struct type of the value to get.</typeparam>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <seealso cref="TryGetClassValue{T}"/>
		/// <seealso cref="TryGetObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		/// <seealso cref="TryGetObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		[Pure]
		public bool TryGetStructValue<T>(BlackboardPropertyName propertyName, out T value) where T : struct
		{
			Profiler.BeginSample("Blackboard.TryGetStructValue<T>");

			if (!m_properties.TryGetValue(propertyName, out ValueIndex valueIndex)
				|| valueIndex.table.valueType != typeof(T))
			{
				value = default;

				Profiler.EndSample();

				return false;
			}

			var table = (BlackboardTable<T>)valueIndex.table;
			value = table.GetValue(valueIndex.index);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Tries to get and return a value of the class type <typeparamref name="T"/>
		/// and the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <typeparam name="T">Class type of the value to get.</typeparam>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <remarks>This method supports derivation.</remarks>
		/// <seealso cref="TryGetStructValue{T}"/>
		/// <seealso cref="TryGetObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		/// <seealso cref="TryGetObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool TryGetClassValue<T>(BlackboardPropertyName propertyName, [CanBeNull] out T value) where T : class
		{
			Profiler.BeginSample("Blackboard.TryGetClassValue<T>");

			bool answer = TryGetObjectValue(typeof(T), propertyName, out object objectValue);
			value = objectValue as T;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Tries to get and return a value of the type <paramref name="valueType"/>
		/// and the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="valueType">Type of the value to get.</param>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <remarks>This method supports derivation.</remarks>
		/// <seealso cref="TryGetStructValue{T}"/>
		/// <seealso cref="TryGetClassValue{T}"/>
		/// <seealso cref="TryGetObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		[Pure]
		public bool TryGetObjectValue([NotNull] Type valueType, BlackboardPropertyName propertyName,
			[CanBeNull] out object value)
		{
			Profiler.BeginSample("Blackboard.TryGetObjectValue typed");

			if (!m_properties.TryGetValue(propertyName, out ValueIndex valueIndex)
				|| !valueType.IsAssignableFrom(valueIndex.table.valueType))
			{
				value = default;

				Profiler.EndSample();

				return false;
			}

			IBlackboardTable table = valueIndex.table;
			value = table.GetObjectValue(valueIndex.index);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Tries to get and return a value of the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <seealso cref="TryGetStructValue{T}"/>
		/// <seealso cref="TryGetClassValue{T}"/>
		/// <seealso cref="TryGetObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		[Pure]
		public bool TryGetObjectValue(BlackboardPropertyName propertyName, [CanBeNull] out object value)
		{
			Profiler.BeginSample("Blackboard.TryGetObjectValue");

			if (!m_properties.TryGetValue(propertyName, out ValueIndex valueIndex))
			{
				value = default;

				Profiler.EndSample();

				return false;
			}

			IBlackboardTable table = valueIndex.table;
			value = table.GetObjectValue(valueIndex.index);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Sets the <paramref name="value"/> of the struct type <typeparamref name="T"/>
		/// and the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <typeparam name="T">Struct value type to set.</typeparam>
		/// <remarks>This method allocates if a property of the type <typeparamref name="T"/> is set first
		/// or when the internal table resizes.</remarks>
		/// <seealso cref="SetClassValue{T}"/>
		/// <seealso cref="SetObjectValue"/>
		public void SetStructValue<T>(BlackboardPropertyName propertyName, T value) where T : struct
		{
			Profiler.BeginSample("Blackboard.SetStructValue<T>");

			Type newType = typeof(T);

			LogSetValue(propertyName, value,"struct");

			if (m_properties.TryGetValue(propertyName, out ValueIndex valueIndex))
			{
				if (valueIndex.table.valueType == newType)
				{
					var table = (BlackboardTable<T>)valueIndex.table;
					table.SetValue(value, valueIndex.index);
				}
				else
				{
					valueIndex.table.Remove(valueIndex.index);
					BlackboardTable<T> table = GetOrCreateTable<T>();
					int index = table.AddValue(value);
					m_properties[propertyName] = new ValueIndex(table, index);
				}
			}
			else
			{
				BlackboardTable<T> table = GetOrCreateTable<T>();
				int index = table.AddValue(value);
				m_properties[propertyName] = new ValueIndex(table, index);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Sets the <paramref name="value"/> of the class type <typeparamref name="T"/>
		/// and the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <typeparam name="T">Class value type to set.</typeparam>
		/// <remarks>This method allocates if a property of the type <typeparamref name="T"/> is set first
		/// or when the internal table resizes.</remarks>
		/// <remarks>
		/// <para>If the <see cref="Blackboard"/> contains a property of the <paramref name="propertyName"/>,
		/// it replaces that property with the new one.</para>
		/// </remarks>
		/// <seealso cref="SetStructValue{T}"/>
		/// <seealso cref="SetObjectValue"/>
		public void SetClassValue<T>(BlackboardPropertyName propertyName, [CanBeNull] T value) where T : class
		{
			Profiler.BeginSample("Blackboard.SetClassValue<T>");

			Type valueType = value == null ? typeof(T) : value.GetType();

			LogSetValue(propertyName, value,"class");

			if (m_properties.TryGetValue(propertyName, out ValueIndex valueIndex))
			{
				if (valueIndex.table.valueType == valueType)
				{
					IBlackboardTable table = valueIndex.table;
					table.SetObjectValue(value, valueIndex.index);
				}
				else
				{
					valueIndex.table.Remove(valueIndex.index);
					IBlackboardTable table = GetOrCreateTable(valueType);
					int index = table.AddObjectValue(value);
					m_properties[propertyName] = new ValueIndex(table, index);
				}
			}
			else
			{
				IBlackboardTable table = GetOrCreateTable(valueType);
				int index = table.AddObjectValue(value);
				m_properties[propertyName] = new ValueIndex(table, index);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Sets the <paramref name="value"/> of the type <paramref name="valueType"/>
		/// and the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="valueType">Value type to set.</param>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <remarks>
		/// <para>This method allocates if a property of the type <paramref name="valueType"/> is set first
		/// or when the internal table resizes.</para>
		/// <para>If the <see cref="Blackboard"/> contains a property of the <paramref name="propertyName"/>,
		/// it replaces that property with the new one.</para>
		/// </remarks>
		/// <seealso cref="SetStructValue{T}"/>
		/// <seealso cref="SetClassValue{T}"/>
		public void SetObjectValue([NotNull] Type valueType, BlackboardPropertyName propertyName,
			[CanBeNull] object value)
		{
			Profiler.BeginSample("Blackboard.SetObjectValue");

			if (value != null)
			{
				valueType = value.GetType();
			}

			LogSetValue(propertyName, value,"object");

			if (m_properties.TryGetValue(propertyName, out ValueIndex valueIndex))
			{
				if (valueIndex.table.valueType == valueType)
				{
					IBlackboardTable table = m_tables[valueType];
					table.SetObjectValue(value, valueIndex.index);
				}
				else
				{
					valueIndex.table.Remove(valueIndex.index);
					IBlackboardTable table = GetOrCreateTable(valueType);
					int index = table.AddObjectValue(value);
					m_properties[propertyName] = new ValueIndex(table, index);
				}
			}
			else
			{
				IBlackboardTable table = GetOrCreateTable(valueType);
				int index = table.AddObjectValue(value);
				m_properties[propertyName] = new ValueIndex(table, index);
			}

			Profiler.EndSample();
		}

		void ICollection<KeyValuePair<BlackboardPropertyName, object>>.Add(KeyValuePair<BlackboardPropertyName, object> item)
		{
			object value = item.Value;
			SetObjectValue(value.GetType(), item.Key, value);
		}

		/// <summary>
		/// Gets all properties of the struct type <typeparamref name="T"/>
		/// and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Found properties are added to this.</param>
		/// <typeparam name="T">Struct value type of the properties to add.</typeparam>
		/// <seealso cref="GetClassProperties{T}"/>
		/// <seealso cref="GetObjectProperties(System.Type,System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,object}})"/>
		/// <seealso cref="GetObjectProperties(System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,object}})"/>
		public void GetStructProperties<T>([NotNull] List<KeyValuePair<BlackboardPropertyName, T>> properties)
			where T : struct
		{
			Profiler.BeginSample("Blackboard.GetStructProperties");

			foreach (KeyValuePair<BlackboardPropertyName, ValueIndex> property in m_properties)
			{
				IBlackboardTable table = property.Value.table;

				if (table.valueType != typeof(T))
				{
					continue;
				}

				var typedTable = (BlackboardTable<T>)table;
				properties.Add(new KeyValuePair<BlackboardPropertyName, T>(property.Key,
					typedTable.GetValue(property.Value.index)));
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all properties of the class type <typeparamref name="T"/>
		/// and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Found properties are added to this.</param>
		/// <typeparam name="T">Class value type of the properties to add.</typeparam>
		/// <seealso cref="GetStructProperties{T}"/>
		/// <seealso cref="GetObjectProperties(System.Type,System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,object}})"/>
		/// <seealso cref="GetObjectProperties(System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,object}})"/>
		public void GetClassProperties<T>([NotNull] List<KeyValuePair<BlackboardPropertyName, T>> properties)
			where T : class
		{
			Profiler.BeginSample("Blackboard.GetClassProperties");

			Type valueType = typeof(T);

			foreach (KeyValuePair<BlackboardPropertyName, ValueIndex> property in m_properties)
			{
				IBlackboardTable table = property.Value.table;

				if (!valueType.IsAssignableFrom(table.valueType))
				{
					continue;
				}

				properties.Add(new KeyValuePair<BlackboardPropertyName, T>(property.Key,
					(T)table.GetObjectValue(property.Value.index)));
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all properties of the type <paramref name="valueType"/>
		/// and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="valueType">Value type of the properties to add.</param>
		/// <param name="properties">Found properties are added to this.</param>
		/// <seealso cref="GetStructProperties{T}"/>
		/// <seealso cref="GetClassProperties{T}"/>
		/// <seealso cref="GetObjectProperties(System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,object}})"/>
		public void GetObjectProperties([NotNull] Type valueType,
			[NotNull] List<KeyValuePair<BlackboardPropertyName, object>> properties)
		{
			Profiler.BeginSample("Blackboard.GetObjectProperties typed");

			foreach (KeyValuePair<BlackboardPropertyName, ValueIndex> property in m_properties)
			{
				IBlackboardTable table = property.Value.table;

				if (!valueType.IsAssignableFrom(table.valueType))
				{
					continue;
				}

				properties.Add(new KeyValuePair<BlackboardPropertyName, object>(property.Key,
					table.GetObjectValue(property.Value.index)));
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all properties and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Properties are added to this.</param>
		/// <seealso cref="GetStructProperties{T}"/>
		/// <seealso cref="GetClassProperties{T}"/>
		/// <seealso cref="GetObjectProperties(System.Type,System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.SimpleBlackboard.Core.BlackboardPropertyName,object}})"/>
		public void GetObjectProperties([NotNull] List<KeyValuePair<BlackboardPropertyName, object>> properties)
		{
			Profiler.BeginSample("Blackboard.GetObjectProperties");

			foreach (KeyValuePair<BlackboardPropertyName, ValueIndex> property in m_properties)
			{
				IBlackboardTable table = property.Value.table;
				properties.Add(new KeyValuePair<BlackboardPropertyName, object>(property.Key,
					table.GetObjectValue(property.Value.index)));
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets value type of a property with the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName"></param>
		/// <returns>
		/// Type of a property with the <paramref name="propertyName"/> or null if such a property is not found.
		/// </returns>
		/// <seealso cref="GetValueTypes"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public Type GetValueType(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.GetValueType");

			m_properties.TryGetValue(propertyName, out ValueIndex valueIndex);
			Type answer = valueIndex.table?.valueType;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Gets all value types contained in the <see cref="Blackboard"/>
		/// and adds them to <paramref name="valueTypes"/>.
		/// </summary>
		/// <param name="valueTypes">Found value types are added to this.</param>
		/// <seealso cref="GetValueType"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetValueTypes([NotNull] List<Type> valueTypes)
		{
			Profiler.BeginSample("Blackboard.GetValueTypes");

			valueTypes.AddRange(m_tables.Keys);

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all property names contained in the <see cref="Blackboard"/>
		/// and adds them to <paramref name="propertyNames"/>.
		/// </summary>
		/// <param name="propertyNames">Found property names are added to this.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetPropertyNames([NotNull] List<BlackboardPropertyName> propertyNames)
		{
			Profiler.BeginSample("Blackboard.GetPropertyNames");

			propertyNames.AddRange(m_properties.Keys);

			Profiler.EndSample();
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of
		/// the <paramref name="propertyName"/> and the struct type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to find.</param>
		/// <typeparam name="T">Struct value type.</typeparam>
		/// <returns>
		/// True if the property is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsObjectValue{T}"/>
		/// <seealso cref="ContainsObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		/// <seealso cref="ContainsObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		[Pure]
		public bool ContainsStructValue<T>(BlackboardPropertyName propertyName) where T : struct
		{
			Profiler.BeginSample("Blackboard.ContainsStructValue<T>");

			bool answer = m_properties.TryGetValue(propertyName, out ValueIndex valueIndex)
				&& valueIndex.table.valueType == typeof(T);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of
		/// the <paramref name="propertyName"/> and the type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to find.</param>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>
		/// True if the property is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsStructValue{T}"/>
		/// <seealso cref="ContainsObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		/// <seealso cref="ContainsObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		[Pure]
		public bool ContainsObjectValue<T>(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.ContainsObjectValue<T>");

			bool answer = m_properties.TryGetValue(propertyName, out ValueIndex valueIndex)
				&& typeof(T).IsAssignableFrom(valueIndex.table.valueType);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of
		/// the <paramref name="propertyName"/> and the type <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <param name="propertyName">Name of the value property to find.</param>
		/// <returns>
		/// True if the property is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsStructValue{T}"/>
		/// <seealso cref="ContainsObjectValue{T}"/>
		/// <seealso cref="ContainsObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		[Pure]
		public bool ContainsObjectValue([NotNull] Type valueType, BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.ContainsObjectValue typed");

			bool answer = m_properties.TryGetValue(propertyName, out ValueIndex valueIndex)
				&& valueType.IsAssignableFrom(valueIndex.table.valueType);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to find.</param>
		/// <returns>
		/// True if the property is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsStructValue{T}"/>
		/// <seealso cref="ContainsObjectValue{T}"/>
		/// <seealso cref="ContainsObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool ContainsObjectValue(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.ContainsObjectValue");

			bool answer = m_properties.ContainsKey(propertyName);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains
		/// or contained a property of the type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>
		/// True if the property is or was contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <remarks>
		/// This method doesn't support derivation.
		/// </remarks>
		/// <seealso cref="ContainsType"/>
		/// <seealso cref="ContainsInheritingType{T}"/>
		/// <seealso cref="ContainsInheritingType"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool ContainsType<T>()
		{
			Profiler.BeginSample("Blackboard.ContainsType<T>");

			bool answer = m_tables.ContainsKey(typeof(T));

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains
		/// or contained a property of the type <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <returns>
		/// True if the property is or was contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <remarks>
		/// This method doesn't support derivation.
		/// </remarks>
		/// <seealso cref="ContainsType{T}"/>
		/// <seealso cref="ContainsInheritingType{T}"/>
		/// <seealso cref="ContainsInheritingType"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool ContainsType([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.ContainsType");

			bool answer = m_tables.ContainsKey(valueType);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains
		/// or contained a property of the type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>
		/// True if the property is or was contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsType{T}"/>
		/// <seealso cref="ContainsType"/>
		/// <seealso cref="ContainsInheritingType"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool ContainsInheritingType<T>()
		{
			Profiler.BeginSample("Blackboard.ContainsInheritingType<T>");

			bool answer = ContainsInheritingType(typeof(T));

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains
		/// or contained a property of the type <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <returns>
		/// True if the property is or was contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsType{T}"/>
		/// <seealso cref="ContainsType"/>
		/// <seealso cref="ContainsInheritingType{T}"/>
		[Pure]
		public bool ContainsInheritingType([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.ContainsInheritingType");

			foreach (Type type in m_tables.Keys)
			{
				if (valueType.IsAssignableFrom(type))
				{
					return true;
				}
			}

			return false;
		}

		bool ICollection<KeyValuePair<BlackboardPropertyName, object>>.Contains(KeyValuePair<BlackboardPropertyName, object> item)
		{
			return TryGetObjectValue(item.Key, out object currentValue) && item.Value.Equals(currentValue);
		}

		/// <summary>
		/// Gets how many properties of the type <typeparamref name="T"/> are contained in the <see cref="Blackboard"/>.
		/// </summary>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>
		/// <para>
		/// How many values of the type <typeparamref name="T"/> are contained in the <see cref="Blackboard"/>.
		/// </para>
		/// <para>
		/// -1 if the <see cref="Blackboard"/> doesn't contain a value of the type <typeparamref name="T"/>.
		/// </para>
		/// </returns>
		/// <remarks>
		/// This method doesn't support derivation.
		/// </remarks>
		/// <seealso cref="GetCount"/>
		/// <seealso cref="GetCountWithInheritors{T}"/>
		/// <seealso cref="GetCountWithInheritors"/>
		[Pure]
		public int GetCount<T>()
		{
			Profiler.BeginSample("Blackboard.GetCount<T>");

			int answer = m_tables.TryGetValue(typeof(T), out IBlackboardTable table)
				? ((BlackboardTable<T>)table).count
				: -1;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Gets how many properties of the type <paramref name="valueType"/>
		/// are contained in the <see cref="Blackboard"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <returns>
		/// <para>
		/// How many values of the type <paramref name="valueType"/> are contained in the <see cref="Blackboard"/>.
		/// </para>
		/// <para>
		/// -1 if the <see cref="Blackboard"/> doesn't contain a value of the type <paramref name="valueType"/>.
		/// </para>
		/// </returns>
		/// <remarks>
		/// This method doesn't support derivation.
		/// </remarks>
		/// <seealso cref="GetCount{T}"/>
		/// <seealso cref="GetCountWithInheritors{T}"/>
		/// <seealso cref="GetCountWithInheritors"/>
		[Pure]
		public int GetCount([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.GetCount");

			int answer = m_tables.TryGetValue(valueType, out IBlackboardTable table)
				? table.count
				: -1;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Gets how many properties of the type <typeparamref name="T"/> are contained in the <see cref="Blackboard"/>.
		/// </summary>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>
		/// <para>
		/// How many values of the type <typeparamref name="T"/> are contained in the <see cref="Blackboard"/>.
		/// </para>
		/// <para>
		/// -1 if the <see cref="Blackboard"/> doesn't contain a value of the type <typeparamref name="T"/>.
		/// </para>
		/// </returns>
		/// <seealso cref="GetCount{T}"/>
		/// <seealso cref="GetCount"/>
		/// <seealso cref="GetCountWithInheritors"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public int GetCountWithInheritors<T>()
		{
			Profiler.BeginSample("Blackboard.GetCountWithInheritors<T>");

			int answer = GetCountWithInheritors(typeof(T));

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Gets how many properties of the type <paramref name="valueType"/>
		/// are contained in the <see cref="Blackboard"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <returns>
		/// <para>
		/// How many values of the type <paramref name="valueType"/> are contained in the <see cref="Blackboard"/>.
		/// </para>
		/// <para>
		/// -1 if the <see cref="Blackboard"/> doesn't contain a value of the type <paramref name="valueType"/>.
		/// </para>
		/// </returns>
		/// <seealso cref="GetCount{T}"/>
		/// <seealso cref="GetCount"/>
		/// <seealso cref="GetCountWithInheritors{T}"/>
		[Pure]
		public int GetCountWithInheritors([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.GetCountWithInheritors");

			bool found = false;
			int count = 0;

			foreach (KeyValuePair<Type, IBlackboardTable> typeTable in m_tables)
			{
				if (valueType.IsAssignableFrom(typeTable.Key))
				{
					found = true;
					count += typeTable.Value.count;
				}
			}

			int answer = found ? count : -1;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Removes a property of the <paramref name="propertyName"/> and the struct type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <typeparam name="T">Struct value type.</typeparam>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <seealso cref="RemoveObject{T}"/>
		/// <seealso cref="RemoveObject(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		/// <seealso cref="RemoveObject(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool RemoveStruct<T>(BlackboardPropertyName propertyName) where T : struct
		{
			Profiler.BeginSample("Blackboard.RemoveStruct<T>");

			Type valueType = typeof(T);

			if (!m_properties.TryGetValue(propertyName, out ValueIndex valueIndex)
				|| valueIndex.table.valueType != valueType)
			{
				Profiler.EndSample();

				return false;
			}

			BlackboardDebug.LogDetails($"[Blackboard] Remove struct of type '{valueType.FullName}' from property '{propertyName}'");

			m_properties.Remove(propertyName);
			var typedTable = (BlackboardTable<T>)valueIndex.table;
			typedTable.Remove(valueIndex.index);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Removes a property of the p<paramref name="propertyName"/> and the type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <seealso cref="RemoveStruct{T}"/>
		/// <seealso cref="RemoveObject(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		/// <seealso cref="RemoveObject(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool RemoveObject<T>(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.RemoveObject<T>");

			Type valueType = typeof(T);

			if (!m_properties.TryGetValue(propertyName, out ValueIndex valueIndex)
				|| !valueType.IsAssignableFrom(valueIndex.table.valueType))
			{
				Profiler.EndSample();

				return false;
			}

			BlackboardDebug.LogDetails($"[Blackboard] Remove object of type '{valueType.FullName}' from property '{propertyName}'");

			m_properties.Remove(propertyName);
			valueIndex.table.Remove(valueIndex.index);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Removes a property of the <paramref name="propertyName"/> and the type <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <seealso cref="RemoveStruct{T}"/>
		/// <seealso cref="RemoveObject{T}"/>
		/// <seealso cref="RemoveObject(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool RemoveObject([NotNull] Type valueType, BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.RemoveObject typed");

			if (!m_properties.TryGetValue(propertyName, out ValueIndex valueIndex)
				|| !valueType.IsAssignableFrom(valueIndex.table.valueType))
			{
				Profiler.EndSample();

				return false;
			}

			BlackboardDebug.LogDetails($"[Blackboard] Remove object of type '{valueType.FullName}' from property '{propertyName}'");

			m_properties.Remove(propertyName);
			valueIndex.table.Remove(valueIndex.index);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Removes a property of the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <seealso cref="RemoveStruct{T}"/>
		/// <seealso cref="RemoveObject{T}"/>
		/// <seealso cref="RemoveObject(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool RemoveObject(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.RemoveObject");

			if (!m_properties.TryGetValue(propertyName, out ValueIndex valueIndex))
			{
				Profiler.EndSample();

				return false;
			}

			BlackboardDebug.LogDetails($"[Blackboard] Remove object from property {propertyName}");

			m_properties.Remove(propertyName);
			valueIndex.table.Remove(valueIndex.index);

			Profiler.EndSample();

			return true;
		}

		bool ICollection<KeyValuePair<BlackboardPropertyName, object>>.Remove(KeyValuePair<BlackboardPropertyName, object> item)
		{
			BlackboardPropertyName propertyName = item.Key;

			if (TryGetObjectValue(propertyName, out object currentValue) && item.Value.Equals(currentValue))
			{
				RemoveObject(propertyName);

				return true;
			}

			return false;
		}

		/// <summary>
		/// Clears of all properties.
		/// </summary>
		public void Clear()
		{
			Profiler.BeginSample("Blackboard.Clear");

			BlackboardDebug.LogDetails("[Blackboard] Clear");

			m_properties.Clear();

			foreach (IBlackboardTable table in m_tables.Values)
			{
				table.Clear();
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Copies its properties into <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">Destination.</param>
		/// <seealso cref="CopyTo(Blackboard,BlackboardPropertyName)"/>
		/// <seealso cref="CopyTo(Blackboard,BlackboardPropertyName[])"/>
		/// <seealso cref="CopyTo{T}(Blackboard,T)"/>
		public void CopyTo([NotNull] Blackboard blackboard)
		{
			Profiler.BeginSample("Blackboard.CopyTo(Blackboard)");

			BlackboardDebug.LogDetails($"[Blackboard] CopyTo(Blackboard)");

			foreach (KeyValuePair<BlackboardPropertyName, ValueIndex> property in m_properties)
			{
				IBlackboardTable table = property.Value.table;
				object value = table.GetObjectValue(property.Value.index);
				blackboard.SetObjectValue(table.valueType, property.Key, value);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Copies a property of the <paramref name="propertyName"/> into <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">Destination.</param>
		/// <param name="propertyName">Property to copy.</param>
		/// <seealso cref="CopyTo(Blackboard)"/>
		/// <seealso cref="CopyTo(Blackboard,BlackboardPropertyName[])"/>
		/// <seealso cref="CopyTo{T}(Blackboard,T)"/>
		public void CopyTo([NotNull] Blackboard blackboard, BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.CopyTo(Blackboard, BlackboardPropertyName)");

			BlackboardDebug.LogDetails($"[Blackboard] CopyTo(Blackboard, BlackboardPropertyName)");

			if (m_properties.TryGetValue(propertyName, out ValueIndex valueIndex))
			{
				IBlackboardTable table = valueIndex.table;
				object value = table.GetObjectValue(valueIndex.index);
				blackboard.SetObjectValue(table.valueType, propertyName, value);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Copies properties of the property names <paramref name="propertyNames"/> into <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">Destination.</param>
		/// <param name="propertyNames">Properties to copy.</param>
		/// <seealso cref="CopyTo(Blackboard)"/>
		/// <seealso cref="CopyTo(Blackboard,BlackboardPropertyName)"/>
		/// <seealso cref="CopyTo{T}(Blackboard,T)"/>
		public void CopyTo([NotNull] Blackboard blackboard, [NotNull] BlackboardPropertyName[] propertyNames)
		{
			Profiler.BeginSample("Blackboard.CopyTo(Blackboard, BlackboardPropertyName[])");

			BlackboardDebug.LogDetails($"[Blackboard] CopyTo(Blackboard, BlackboardPropertyName[])");

			for (int i = 0, count = propertyNames.Length; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];

				if (m_properties.TryGetValue(propertyName, out ValueIndex valueIndex))
				{
					IBlackboardTable table = valueIndex.table;
					object value = table.GetObjectValue(valueIndex.index);
					blackboard.SetObjectValue(table.valueType, propertyName, value);
				}
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Copies properties of the property names <paramref name="propertyNames"/> into <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">Destination.</param>
		/// <param name="propertyNames">Properties to copy.</param>
		/// <seealso cref="CopyTo(Blackboard)"/>
		/// <seealso cref="CopyTo(Blackboard,BlackboardPropertyName)"/>
		/// <seealso cref="CopyTo(Blackboard,BlackboardPropertyName[])"/>
		public void CopyTo<T>([NotNull] Blackboard blackboard, [NotNull] T propertyNames)
			where T : IList<BlackboardPropertyName>
		{
			Profiler.BeginSample("Blackboard.CopyTo(Blackboard, BlackboardPropertyName[])");

			BlackboardDebug.LogDetails($"[Blackboard] CopyTo(Blackboard, BlackboardPropertyName[])");

			for (int i = 0, count = propertyNames.Count; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];

				if (m_properties.TryGetValue(propertyName, out ValueIndex valueIndex))
				{
					IBlackboardTable table = valueIndex.table;
					object value = table.GetObjectValue(valueIndex.index);
					blackboard.SetObjectValue(table.valueType, propertyName, value);
				}
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Copies its properties into the <paramref name="array"/> starting at the <paramref name="index"/>.
		/// </summary>
		/// <param name="array">Destination.</param>
		/// <param name="index">Starting index.</param>
		/// <seealso cref="CopyTo(object[],int)"/>
		/// <seealso cref="CopyTo(Array,int)"/>
		public void CopyTo(KeyValuePair<BlackboardPropertyName, object>[] array, int index)
		{
			Profiler.BeginSample("Blackboard.CopyTo(KeyValuePair[], int)");

			foreach (KeyValuePair<BlackboardPropertyName, ValueIndex> property in m_properties)
			{
				IBlackboardTable table = property.Value.table;
				object value = table.GetObjectValue(property.Value.index);
				array[index++] = new KeyValuePair<BlackboardPropertyName, object>(property.Key, value);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Copies its properties into the <paramref name="array"/> starting at the <paramref name="index"/>.
		/// </summary>
		/// <param name="array">Destination.</param>
		/// <param name="index">Starting index.</param>
		/// <seealso cref="CopyTo(KeyValuePair{BlackboardPropertyName,object}[],int)"/>
		/// <seealso cref="CopyTo(Array,int)"/>
		public void CopyTo([NotNull] object[] array, int index)
		{
			Profiler.BeginSample("Blackboard.CopyTo(object[], int)");

			foreach (KeyValuePair<BlackboardPropertyName, ValueIndex> property in m_properties)
			{
				IBlackboardTable table = property.Value.table;
				object value = table.GetObjectValue(property.Value.index);
				array[index++] = new KeyValuePair<BlackboardPropertyName, object>(property.Key, value);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Copies its properties into the <paramref name="array"/> starting at the <paramref name="index"/>.
		/// </summary>
		/// <param name="array">Destination.</param>
		/// <param name="index">Starting index.</param>
		/// <remarks>
		/// Arrays of types <see cref="KeyValuePair{BlackboardPropertyName,Object}"/> and <see cref="object"/>
		/// are supported only.
		/// </remarks>
		/// <seealso cref="CopyTo(KeyValuePair{BlackboardPropertyName,object}[],int)"/>
		/// <seealso cref="CopyTo(object[],int)"/>
		public void CopyTo(Array array, int index)
		{
			Profiler.BeginSample("Blackboard.CopyTo(Array, int)");

			switch (array)
			{
				case KeyValuePair<BlackboardPropertyName, object>[] pairs:
				{
					CopyTo(pairs, index);
					break;
				}
				case object[] objects:
				{
					CopyTo(objects, index);
					break;
				}
				default:
					BlackboardDebug.LogWarning($"[Blackboard] Can't copy to the array because its type is not '{typeof(KeyValuePair<BlackboardPropertyName, object>).FullName}' or '{typeof(object).FullName}'");
					break;
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets an enumerator of all properties of <see cref="Blackboard"/>.
		/// </summary>
		/// <returns>
		/// Enumerator of all properties of <see cref="Blackboard"/>.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator<KeyValuePair<BlackboardPropertyName, object>> IEnumerable<KeyValuePair<BlackboardPropertyName, object>>.GetEnumerator()
		{
			return GetEnumerator();
		}

		[Pure]
		public override string ToString()
		{
			var builder = new StringBuilder();

			foreach (KeyValuePair<BlackboardPropertyName, ValueIndex> property in m_properties)
			{
				builder.Append($"{{{property.Key}, {property.Value.table.GetObjectValue(property.Value.index)}}},\n");
			}

			int builderLength = builder.Length;
			int length = builderLength >= 2 ? builderLength - 2 : 0;

			return builder.ToString(0, length);
		}

		/// <summary>
		/// Creates a new table of the type <typeparamref name="T"/> and adds it to <see cref="m_tables"/>.
		/// </summary>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>Created table.</returns>
		/// <seealso cref="CreateTable"/>
		[NotNull]
		private BlackboardTable<T> CreateTable<T>()
		{
			Profiler.BeginSample("Blackboard.CreateTable<T>");

			Type valueType = typeof(T);

			BlackboardDebug.Log($"[Blackboard] Create blackboard table of type '{valueType.FullName}'");

			var table = new BlackboardTable<T>();
			m_tables.Add(valueType, table);

			Profiler.EndSample();

			return table;
		}

		/// <summary>
		/// Creates a new table of the type <paramref name="valueType"/> and adds it to <see cref="m_tables"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <returns>Created table.</returns>
		/// <remarks>If it's possible, use <see cref="CreateTable{T}"/> because this method is more expensive.</remarks>
		/// <seealso cref="CreateTable{T}"/>
		[NotNull]
		private IBlackboardTable CreateTable([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.CreateTable");

			Type tableType = typeof(BlackboardTable<>).MakeGenericType(valueType);

			BlackboardDebug.Log($"[Blackboard] Create blackboard table of type '{valueType.FullName}'");

			var table = (IBlackboardTable)Activator.CreateInstance(tableType);
			m_tables.Add(valueType, table);

			Profiler.EndSample();

			return table;
		}

		/// <summary>
		/// Creates a new table of the type <typeparamref name="T"/> and adds it to <see cref="m_tables"/>
		/// or returns a current table if it exists.
		/// </summary>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>Created or existing table.</returns>
		/// <seealso cref="GetOrCreateTable"/>
		[NotNull]
		private BlackboardTable<T> GetOrCreateTable<T>()
		{
			Profiler.BeginSample("Blackboard.GetOrCreateTable<T>");

			BlackboardTable<T> answer = m_tables.TryGetValue(typeof(T), out IBlackboardTable table)
				? (BlackboardTable<T>)table
				: CreateTable<T>();

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Creates a new table of the type <paramref name="valueType"/> and adds it to <see cref="m_tables"/>
		/// or returns a current table if it exists.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <returns>Created or existing table.</returns>
		/// <remarks>
		/// If it's possible, use <see cref="GetOrCreateTable{T}"/> because this method is more expensive.
		/// </remarks>
		/// <seealso cref="GetOrCreateTable{T}"/>
		[NotNull]
		private IBlackboardTable GetOrCreateTable([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.GetOrCreateTable");

			if (!m_tables.TryGetValue(valueType, out IBlackboardTable table))
			{
				table = CreateTable(valueType);
			}

			Profiler.EndSample();

			return table;
		}

		[Conditional(BlackboardDebug.LogDetailsDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void LogSetValue<T>(BlackboardPropertyName propertyName, T value, string valuePrefix)
		{
			string valueText;

			try
			{
				valueText = value.ToString();
			}
			catch (Exception)
			{
				valueText = "unknown";
			}

			BlackboardDebug.LogDetails($"[Blackboard] Set {valuePrefix} value '{valueText}' of type '{typeof(T).FullName}' into property '{propertyName}'");
		}

		/// <summary>
		/// Enumerator of all properties of a <see cref="Blackboard"/>.
		/// </summary>
		public struct Enumerator : IEnumerator<KeyValuePair<BlackboardPropertyName, object>>
		{
			[NotNull] private readonly Blackboard m_blackboard;
			private Dictionary<BlackboardPropertyName, ValueIndex>.Enumerator m_enumerator;
			private KeyValuePair<BlackboardPropertyName, object> m_current;

			internal Enumerator([NotNull] Blackboard blackboard)
			{
				m_blackboard = blackboard;
				m_enumerator = m_blackboard.m_properties.GetEnumerator();
				m_current = new KeyValuePair<BlackboardPropertyName, object>();
			}

			/// <inheritdoc/>
			public KeyValuePair<BlackboardPropertyName, object> Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
				get => m_current;
			}

			/// <inheritdoc/>
			object IEnumerator.Current =>
				new KeyValuePair<BlackboardPropertyName, object>(m_current.Key, m_current.Value);

			/// <inheritdoc/>
			public bool MoveNext()
			{
				Profiler.BeginSample("Blackboard.Enumerator.MoveNext");

				if (!m_enumerator.MoveNext())
				{
					Profiler.EndSample();

					return false;
				}

				KeyValuePair<BlackboardPropertyName, ValueIndex> current = m_enumerator.Current;
				BlackboardPropertyName propertyName = current.Key;
				m_current = new KeyValuePair<BlackboardPropertyName, object>(
					propertyName,
					current.Value.table.GetObjectValue(current.Value.index));

				Profiler.EndSample();

				return true;
			}

			/// <inheritdoc/>
			void IEnumerator.Reset()
			{
				m_enumerator.Dispose();
				m_enumerator = m_blackboard.m_properties.GetEnumerator();
				m_current = new KeyValuePair<BlackboardPropertyName, object>();
			}

			/// <inheritdoc/>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
				m_enumerator.Dispose();
			}
		}

		/// <summary>
		/// Struct that holds a ref to a <see cref="IBlackboardTable"/> and index of a value in it.
		/// </summary>
		private readonly struct ValueIndex
		{
			public readonly IBlackboardTable table;
			public readonly int index;

			public ValueIndex(IBlackboardTable table, int index)
			{
				this.table = table;
				this.index = index;
			}
		}
	}
}
