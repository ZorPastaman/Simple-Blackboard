// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;

namespace Zor.SimpleBlackboard.Core
{
	/// <summary>
	/// <para>Interface for an entry of <see cref="Zor.SimpleBlackboard.Core.Blackboard"/>.</para>
	/// <para>The only implementor is <see cref="BlackboardTable{T}"/>.</para>
	/// </summary>
	internal interface IBlackboardTable
	{
		/// <summary>
		/// Type of values that are contained in the <see cref="IBlackboardTable"/>.
		/// </summary>
		[NotNull]
		Type valueType { [Pure] get; }

		/// <summary>
		/// How many values are contained in the <see cref="IBlackboardTable"/>.
		/// </summary>
		int count { [Pure] get; }

		/// <summary>
		/// Gets a value by the <paramref name="index"/>
		/// </summary>
		/// <param name="index">Index of the property to get.</param>
		/// <returns>Gotten value.</returns>
		[CanBeNull, Pure]
		object GetObjectValue(int index);

		/// <summary>
		/// Sets the <paramref name="value"/> by the <paramref name="index"/>.
		/// </summary>
		/// <param name="value">Value to set.</param>
		/// <param name="index">Index of the property to set.</param>
		void SetObjectValue([CanBeNull] object value, int index);

		/// <summary>
		/// Adds the <paramref name="value"/> to the table.
		/// </summary>
		/// <param name="value">Value to add.</param>
		/// <returns>Value index.</returns>
		int AddObjectValue([CanBeNull] object value);

		/// <summary>
		/// Removes a property by the <paramref name="index"/>.
		/// </summary>
		/// <param name="index">Index of the property to remove.</param>
		void Remove(int index);

		/// <summary>
		/// Clears of all properties.
		/// </summary>
		void Clear();
	}
}
