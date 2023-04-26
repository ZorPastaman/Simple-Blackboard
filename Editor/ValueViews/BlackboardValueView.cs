// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// <para>View for a blackboard value.</para>
	/// <para>Inherit this to make a drawer for a value of
	/// <see cref="Zor.SimpleBlackboard.Core.Blackboard"/>.</para>
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <remarks>
	/// Blackboard values are drawn by this class because Unity draws properties by itself only if the property is
	/// serialized by Unity which is not true for the Blackboard system.
	/// </remarks>
	public abstract class BlackboardValueView<T> : IBlackboardValueView
	{
		/// <inheritdoc/>
		public Type valueType
		{
			[Pure]
			get => typeof(T);
		}

		/// <inheritdoc/>
		/// <summary>
		/// Creates a <see cref="VisualElement"/> to represent a property of type <typeparamref name="T"/>.
		/// </summary>
		[Pure]
		public VisualElement CreateVisualElement(string label)
		{
			return CreateBaseField(label);
		}

		/// <summary>
		/// Creates a <see cref="BaseField{TValueType}"/> to represent a property of type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="label">Label of a created <see cref="BaseField{TValueType}"/>.</param>
		/// <returns><see cref="BaseField{TValueType}"/> that represents a property.</returns>
		[NotNull, Pure]
		public abstract BaseField<T> CreateBaseField([CanBeNull] string label);

		/// <summary>
		/// Draws <paramref name="value"/> in the editor and returns new value.
		/// </summary>
		/// <param name="label">Label which is used in the editor.</param>
		/// <param name="value">Current value.</param>
		/// <returns>New value.</returns>
		[CanBeNull, Pure]
		public abstract T DrawValue([NotNull] string label, [CanBeNull] T value);
	}
}
