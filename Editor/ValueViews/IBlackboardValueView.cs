// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Interface for <see cref="BlackboardValueView{T}"/>.
	/// </summary>
	internal interface IBlackboardValueView
	{
		/// <summary>
		/// Value type of the drawn property.
		/// </summary>
		[NotNull]
		Type valueType { get; }

		/// <summary>
		/// Creates a <see cref="VisualElement"/> to represent a property.
		/// </summary>
		/// <param name="label">Label of a created <see cref="VisualElement"/>.</param>
		/// <returns><see cref="VisualElement"/> that represents a property.</returns>
		[NotNull]
		VisualElement CreateVisualElement([CanBeNull] string label);
	}
}
