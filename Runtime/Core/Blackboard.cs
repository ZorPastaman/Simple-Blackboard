// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.SimpleBlackboard.Debugging;

namespace Zor.SimpleBlackboard.Core
{
	/// <summary>
	/// The main class of the Blackboard system.
	/// </summary>
	public sealed class Blackboard
	{
		/// <summary>
		/// Value type to table of that type dictionary.
		/// </summary>
		private readonly Dictionary<Type, IBlackboardTable> m_tables = new Dictionary<Type, IBlackboardTable>();
		/// <summary>
		/// Property name to value type of that property dictionary.
		/// </summary>
		private readonly Dictionary<BlackboardPropertyName, Type> m_propertyTypes =
			new Dictionary<BlackboardPropertyName, Type>();

		/// <summary>
		/// How many value types are contained in the <see cref="Blackboard"/>.
		/// </summary>
		public int valueTypesCount => m_tables.Count;

		/// <summary>
		/// How many properties are contained in the <see cref="Blackboard"/>.
		/// </summary>
		public int propertiesCount => m_propertyTypes.Count;

		/// <summary>
		/// Tries to get and return a value of the struct type <typeparamref name="T"/>
		/// and the property name <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <typeparam name="T">Struct type of the value to get.</typeparam>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <seealso cref="TryGetClassValue{T}"/>
		/// <seealso cref="TryGetObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		/// <seealso cref="TryGetObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		public bool TryGetStructValue<T>(BlackboardPropertyName propertyName, out T value) where T : struct
		{
			Profiler.BeginSample("Blackboard.TryGetStructValue<T>");

			if (!m_propertyTypes.TryGetValue(propertyName, out Type currentType)
				|| currentType != typeof(T))
			{
				value = default;

				Profiler.EndSample();

				return false;
			}

			var table = (BlackboardTable<T>)m_tables[currentType];
			value = table.GetValue(propertyName);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Tries to get and return a value of the class type <typeparamref name="T"/>
		/// and the property name <paramref name="propertyName"/>.
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
		public bool TryGetClassValue<T>(BlackboardPropertyName propertyName, out T value) where T : class
		{
			Profiler.BeginSample("Blackboard.TryGetClassValue<T>");

			bool answer = TryGetObjectValue(typeof(T), propertyName, out object objectValue);
			value = objectValue as T;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Tries to get and return a value of the type <paramref name="valueType"/>
		/// and the property name <paramref name="propertyName"/>.
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
		public bool TryGetObjectValue([NotNull] Type valueType, BlackboardPropertyName propertyName, out object value)
		{
			Profiler.BeginSample("Blackboard.TryGetObjectValue typed");

			if (!m_propertyTypes.TryGetValue(propertyName, out Type currentType)
				|| !valueType.IsAssignableFrom(currentType))
			{
				value = default;

				Profiler.EndSample();

				return false;
			}

			IBlackboardTable table = m_tables[currentType];
			value = table.GetObjectValue(propertyName);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Tries to get and return a value of the property name <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <seealso cref="TryGetStructValue{T}"/>
		/// <seealso cref="TryGetClassValue{T}"/>
		/// <seealso cref="TryGetObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName,out object)"/>
		public bool TryGetObjectValue(BlackboardPropertyName propertyName, out object value)
		{
			Profiler.BeginSample("Blackboard.TryGetObjectValue");

			if (!m_propertyTypes.TryGetValue(propertyName, out Type currentType))
			{
				value = default;

				Profiler.EndSample();

				return false;
			}

			IBlackboardTable table = m_tables[currentType];
			value = table.GetObjectValue(propertyName);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Sets the value <paramref name="value"/> of the struct type <typeparamref name="T"/>
		/// and the property name <paramref name="propertyName"/>.
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
			BlackboardTable<T> table;

			BlackboardDebug.LogDetails($"[Blackboard] Set struct value '{value}' of type '{newType.FullName}' into property '{propertyName}'");

			if (m_propertyTypes.TryGetValue(propertyName, out Type currentType))
			{
				if (currentType == newType)
				{
					table = (BlackboardTable<T>)m_tables[newType];
				}
				else
				{
					m_tables[currentType].Remove(propertyName);
					m_propertyTypes[propertyName] = newType;
					table = GetOrCreateTable<T>();
				}
			}
			else
			{
				m_propertyTypes[propertyName] = newType;
				table = GetOrCreateTable<T>();
			}

			table.SetValue(propertyName, value);

			Profiler.EndSample();
		}

		/// <summary>
		/// Sets the value <paramref name="value"/> of the class type <typeparamref name="T"/>
		/// and the property name <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <typeparam name="T">Class value type to set.</typeparam>
		/// <remarks>This method allocates if a property of the type <typeparamref name="T"/> is set first
		/// or when the internal table resizes.</remarks>
		/// <remarks>
		/// <para>If the <see cref="Blackboard"/> contains a property of the property name <paramref name="propertyName"/>,
		/// it replaces that property with the new one.</para>
		/// </remarks>
		/// <seealso cref="SetStructValue{T}"/>
		/// <seealso cref="SetObjectValue"/>
		public void SetClassValue<T>(BlackboardPropertyName propertyName, [CanBeNull] T value) where T : class
		{
			Profiler.BeginSample("Blackboard.SetClassValue<T>");

			Type valueType = value == null ? typeof(T) : value.GetType();
			IBlackboardTable table;

			BlackboardDebug.LogDetails($"[Blackboard] Set class value '{value}' of type '{valueType.FullName}' into property '{propertyName}'");

			if (m_propertyTypes.TryGetValue(propertyName, out Type currentType))
			{
				if (currentType == valueType)
				{
					table = m_tables[valueType];
				}
				else
				{
					m_tables[currentType].Remove(propertyName);
					m_propertyTypes[propertyName] = valueType;
					table = GetOrCreateTable(valueType);
				}
			}
			else
			{
				m_propertyTypes[propertyName] = valueType;
				table = GetOrCreateTable(valueType);
			}

			table.SetObjectValue(propertyName, value);

			Profiler.EndSample();
		}

		/// <summary>
		/// Sets the value <paramref name="value"/> of the type <paramref name="valueType"/>
		/// and the property name <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="valueType">Value type to set.</param>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <remarks>
		/// <para>This method allocates if a property of the type <paramref name="valueType"/> is set first
		/// or when the internal table resizes.</para>
		/// <para>If the <see cref="Blackboard"/> contains a property of the property name <paramref name="propertyName"/>,
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

			IBlackboardTable table;

			BlackboardDebug.LogDetails($"[Blackboard] Set object value '{value}' of type '{valueType.FullName}' into property '{propertyName}'");

			if (m_propertyTypes.TryGetValue(propertyName, out Type currentType))
			{
				if (currentType == valueType)
				{
					table = m_tables[valueType];
				}
				else
				{
					m_tables[currentType].Remove(propertyName);
					m_propertyTypes[propertyName] = valueType;
					table = GetOrCreateTable(valueType);
				}
			}
			else
			{
				m_propertyTypes[propertyName] = valueType;
				table = GetOrCreateTable(valueType);
			}

			table.SetObjectValue(propertyName, value);

			Profiler.EndSample();
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

			if (m_tables.TryGetValue(typeof(T), out IBlackboardTable table))
			{
				var typedTable = (BlackboardTable<T>)table;
				typedTable.GetProperties(properties);
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

			Dictionary<Type, IBlackboardTable>.Enumerator enumerator = m_tables.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<Type, IBlackboardTable> current = enumerator.Current;

				if (valueType.IsAssignableFrom(current.Key))
				{
					current.Value.GetPropertiesAs(properties);
				}
			}
			enumerator.Dispose();

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

			Dictionary<Type, IBlackboardTable>.Enumerator enumerator = m_tables.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<Type, IBlackboardTable> current = enumerator.Current;

				if (valueType.IsAssignableFrom(current.Key))
				{
					current.Value.GetProperties(properties);
				}
			}
			enumerator.Dispose();

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

			Dictionary<Type, IBlackboardTable>.ValueCollection.Enumerator enumerator = m_tables.Values.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.GetProperties(properties);
			}
			enumerator.Dispose();

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all value types contained in the <see cref="Blackboard"/>
		/// and adds them to <paramref name="valueTypes"/>.
		/// </summary>
		/// <param name="valueTypes">Found value types are added to this.</param>
		public void GetValueTypes([NotNull] List<Type> valueTypes)
		{
			Profiler.BeginSample("Blackboard.GetValueTypes");

			valueTypes.AddRange(m_tables.Keys);

			Profiler.EndSample();
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of
		/// the property name <paramref name="propertyName"/> and the struct type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to find.</param>
		/// <typeparam name="T">Struct value type.</typeparam>
		/// <returns>
		/// True if the property is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsObjectValue{T}"/>
		/// <seealso cref="ContainsObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		/// <seealso cref="ContainsObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool ContainsStructValue<T>(BlackboardPropertyName propertyName) where T : struct
		{
			Profiler.BeginSample("Blackboard.ContainsStructValue<T>");

			bool answer = m_propertyTypes.TryGetValue(propertyName, out Type currentType) && currentType == typeof(T);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of
		/// the property name <paramref name="propertyName"/> and the type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to find.</param>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>
		/// True if the property is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsStructValue{T}"/>
		/// <seealso cref="ContainsObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		/// <seealso cref="ContainsObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool ContainsObjectValue<T>(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.ContainsObjectValue<T>");

			bool answer = m_propertyTypes.TryGetValue(propertyName, out Type currentType)
				&& typeof(T).IsAssignableFrom(currentType);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of
		/// the property name <paramref name="propertyName"/> and the type <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <param name="propertyName">Name of the value property to find.</param>
		/// <returns>
		/// True if the property is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsStructValue{T}"/>
		/// <seealso cref="ContainsObjectValue{T}"/>
		/// <seealso cref="ContainsObjectValue(Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool ContainsObjectValue([NotNull] Type valueType, BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.ContainsObjectValue typed");

			bool answer = m_propertyTypes.TryGetValue(propertyName, out Type currentType)
				&& valueType.IsAssignableFrom(currentType);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of
		/// the property name <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to find.</param>
		/// <returns>
		/// True if the property is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsStructValue{T}"/>
		/// <seealso cref="ContainsObjectValue{T}"/>
		/// <seealso cref="ContainsObjectValue(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool ContainsObjectValue(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.ConstainsObjectValue");

			bool answer = m_propertyTypes.ContainsKey(propertyName);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains or contained
		/// a property of the type <typeparamref name="T"/>.
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
		public bool ContainsType<T>()
		{
			Profiler.BeginSample("Blackboard.ContainsType<T>");

			bool answer = m_tables.ContainsKey(typeof(T));

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains or contained
		/// a property of the type <paramref name="valueType"/>.
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
		public bool ContainsType([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.ContainsType");

			bool answer = m_tables.ContainsKey(valueType);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains or contained
		/// a property of the type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>
		/// True if the property is or was contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsType{T}"/>
		/// <seealso cref="ContainsType"/>
		/// <seealso cref="ContainsInheritingType"/>
		public bool ContainsInheritingType<T>()
		{
			Profiler.BeginSample("Blackboard.ContainsInheritingType<T>");

			bool answer = ContainsInheritingType(typeof(T));

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains or contained
		/// a property of the type <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Value type.</param>
		/// <returns>
		/// True if the property is or was contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="ContainsType{T}"/>
		/// <seealso cref="ContainsType"/>
		/// <seealso cref="ContainsInheritingType{T}"/>
		public bool ContainsInheritingType([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.ContainsInheritingType");

			bool answer = false;

			Dictionary<Type, IBlackboardTable>.KeyCollection.Enumerator enumerator = m_tables.Keys.GetEnumerator();
			while (enumerator.MoveNext() && !answer)
			{
				answer = valueType.IsAssignableFrom(enumerator.Current);
			}
			enumerator.Dispose();

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Gets how many properties of the type <typeparamref name="T"/> are contained in the <see cref="Blackboard"/>.
		/// </summary>
		/// <typeparam name="T">Value type.</typeparam>
		/// <returns>
		/// <para>How many values of the type <typeparamref name="T"/>
		/// are contained in the <see cref="Blackboard"/>.</para>
		/// <para>-1 if the <see cref="Blackboard"/> doesn't contain a value
		/// of the type <typeparamref name="T"/>.</para>
		/// </returns>
		/// <remarks>
		/// This method doesn't support derivation.
		/// </remarks>
		/// <seealso cref="GetCount"/>
		/// <seealso cref="GetCountWithInheritors{T}"/>
		/// <seealso cref="GetCountWithInheritors"/>
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
		/// <para>How many values of the type <paramref name="valueType"/>
		/// are contained in the <see cref="Blackboard"/>.</para>
		/// <para>-1 if the <see cref="Blackboard"/> doesn't contain a value
		/// of the type <paramref name="valueType"/>.</para>
		/// </returns>
		/// <remarks>
		/// This method doesn't support derivation.
		/// </remarks>
		/// <seealso cref="GetCount{T}"/>
		/// <seealso cref="GetCountWithInheritors{T}"/>
		/// <seealso cref="GetCountWithInheritors"/>
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
		/// <para>How many values of the type <typeparamref name="T"/>
		/// are contained in the <see cref="Blackboard"/>.</para>
		/// <para>-1 if the <see cref="Blackboard"/> doesn't contain a value
		/// of the type <typeparamref name="T"/>.</para>
		/// </returns>
		/// <seealso cref="GetCount{T}"/>
		/// <seealso cref="GetCount"/>
		/// <seealso cref="GetCountWithInheritors"/>
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
		/// <para>How many values of the type <paramref name="valueType"/>
		/// are contained in the <see cref="Blackboard"/>.</para>
		/// <para>-1 if the <see cref="Blackboard"/> doesn't contain a value
		/// of the type <paramref name="valueType"/>.</para>
		/// </returns>
		/// <seealso cref="GetCount{T}"/>
		/// <seealso cref="GetCount"/>
		/// <seealso cref="GetCountWithInheritors{T}"/>
		public int GetCountWithInheritors([NotNull] Type valueType)
		{
			Profiler.BeginSample("Blackboard.GetCountWithInheritors");

			bool found = false;
			int count = 0;

			Dictionary<Type, IBlackboardTable>.Enumerator enumerator = m_tables.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<Type, IBlackboardTable> current = enumerator.Current;

				if (valueType.IsAssignableFrom(current.Key))
				{
					found = true;
					count += current.Value.count;
				}
			}
			enumerator.Dispose();

			int answer = found ? count : -1;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Removes a property of the property name <paramref name="propertyName"/>
		/// and the struct type <typeparamref name="T"/>.
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

			if (!m_propertyTypes.TryGetValue(propertyName, out Type currentType)
				|| currentType != valueType)
			{
				Profiler.EndSample();

				return false;
			}

			BlackboardDebug.LogDetails($"[Blackboard] Remove struct of type '{valueType.FullName}' from property '{propertyName}'");

			m_propertyTypes.Remove(propertyName);
			var typedTable = (BlackboardTable<T>)m_tables[currentType];
			typedTable.Remove(propertyName);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Removes a property of the property name <paramref name="propertyName"/> and
		/// the type <typeparamref name="T"/>.
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

			if (!m_propertyTypes.TryGetValue(propertyName, out Type currentType)
				|| !valueType.IsAssignableFrom(currentType))
			{
				Profiler.EndSample();

				return false;
			}

			BlackboardDebug.LogDetails($"[Blackboard] Remove object of type '{valueType.FullName}' from property '{propertyName}'");

			m_propertyTypes.Remove(propertyName);
			m_tables[currentType].Remove(propertyName);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Removes a property of the property name <paramref name="propertyName"/>
		/// and the type <paramref name="valueType"/>.
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

			if (!m_propertyTypes.TryGetValue(propertyName, out Type currentType)
				|| !valueType.IsAssignableFrom(currentType))
			{
				Profiler.EndSample();

				return false;
			}

			BlackboardDebug.LogDetails($"[Blackboard] Remove object of type '{valueType.FullName}' from property '{propertyName}'");

			m_propertyTypes.Remove(propertyName);
			m_tables[currentType].Remove(propertyName);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Removes a property of the property name <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <seealso cref="RemoveStruct{T}"/>
		/// <seealso cref="RemoveObject{T}"/>
		/// <seealso cref="RemoveObject(System.Type,Zor.SimpleBlackboard.Core.BlackboardPropertyName)"/>
		public bool RemoveObject(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("Blackboard.RemoveObject");

			if (!m_propertyTypes.TryGetValue(propertyName, out Type currentType))
			{
				Profiler.EndSample();

				return false;
			}

			BlackboardDebug.LogDetails($"[Blackboard] Remove object from property {propertyName}");

			m_propertyTypes.Remove(propertyName);
			m_tables[currentType].Remove(propertyName);

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Clears of all properties.
		/// </summary>
		public void Clear()
		{
			Profiler.BeginSample("Blackboard.Clear");

			BlackboardDebug.LogDetails("[Blackboard] Clear");

			m_propertyTypes.Clear();

			Dictionary<Type, IBlackboardTable>.ValueCollection.Enumerator enumerator = m_tables.Values.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.Clear();
			}
			enumerator.Dispose();

			Profiler.EndSample();
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			Dictionary<Type, IBlackboardTable>.Enumerator enumerator = m_tables.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<Type, IBlackboardTable> current = enumerator.Current;
				builder.Append($"{{{current.Key.FullName}, {current.Value}}},\n");
			}
			enumerator.Dispose();

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
	}
}
