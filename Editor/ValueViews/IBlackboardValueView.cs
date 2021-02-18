// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

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

		[NotNull]
		VisualElement CreateVisualElement([NotNull] string label, VisualElement blackboardRoot = null);

		void SetValue([NotNull] string key, [NotNull] VisualElement visualElement, [NotNull] Blackboard blackboard);
	}
}
