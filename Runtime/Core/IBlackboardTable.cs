// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Zor.SimpleBlackboard.Core
{
	internal interface IBlackboardTable
	{
		/// <summary>
		/// Type of values that are contained in the <see cref="IBlackboardTable"/>.
		/// </summary>
		[NotNull]
		Type valueType { get; }

		/// <summary>
		/// How many values are contained in the <see cref="IBlackboardTable"/>.
		/// </summary>
		int count { get; }

		/// <summary>
		/// Gets a value of the property name <paramref name="propertyName"/>
		/// </summary>
		/// <param name="propertyName">Name of the property to get.</param>
		/// <returns>Gotten value.</returns>
		[CanBeNull, Pure]
		object GetObjectValue(BlackboardPropertyName propertyName);

		/// <summary>
		/// Sets the value <paramref name="value"/> of the property name <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to set.</param>
		/// <param name="value">Value to set.</param>
		void SetObjectValue(BlackboardPropertyName propertyName, [CanBeNull] object value);

		/// <summary>
		/// Gets all properties and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Properties are added to this.</param>
		void GetProperties([NotNull] List<KeyValuePair<BlackboardPropertyName, object>> properties);

		/// <summary>
		/// Gets all properties, casts their values to <typeparamref name="TAs"/> and
		/// adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Properties are added to this.</param>
		/// <typeparam name="TAs">Cast value type.</typeparam>
		void GetPropertiesAs<TAs>([NotNull] List<KeyValuePair<BlackboardPropertyName, TAs>> properties)
			where TAs : class;

		/// <summary>
		/// Removes a property of the <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		bool Remove(BlackboardPropertyName propertyName);

		/// <summary>
		/// Clears of all properties.
		/// </summary>
		void Clear();

		/// <summary>
		/// Copies its properties to <paramref name="table"/>.
		/// </summary>
		/// <param name="table">Destination. Must be the same type.</param>
		void CopyTo([NotNull] IBlackboardTable table);
	}
}
