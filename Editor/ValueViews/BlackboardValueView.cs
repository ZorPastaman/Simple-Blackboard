// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

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
		public Type valueType => typeof(T);

		public abstract VisualElement CreateVisualElement(string label);

		public abstract void SetValue(string key, VisualElement visualElement, Blackboard blackboard);

		/// <summary>
		/// Draws <paramref name="value"/> in the editor and returns new value.
		/// </summary>
		/// <param name="label">Label which is used in the editor.</param>
		/// <param name="value">Current value.</param>
		/// <returns>New value.</returns>
		[CanBeNull]
		public abstract T DrawValue([NotNull] string label, [CanBeNull] T value);
	}
}
