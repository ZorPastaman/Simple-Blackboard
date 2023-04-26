// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Component"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class ComponentBlackboardValueView : UnityObjectBlackboardValueView<Component>
	{
	}
}
