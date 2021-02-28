// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Collections.Generic;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.BlackboardValueViews;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <inheritdoc/>
	internal sealed class StructBlackboardTableEditor<T> : BlackboardTableEditor<T> where T : struct
	{
		public StructBlackboardTableEditor(BlackboardValueView<T> blackboardValueView) : base(blackboardValueView)
		{
		}

		/// <inheritdoc/>
		protected override void GetProperties(Blackboard blackboard,
			List<KeyValuePair<BlackboardPropertyName, T>> properties)
		{
			blackboard.GetStructProperties(properties);
		}

		/// <inheritdoc/>
		protected override void SetValue(Blackboard blackboard, BlackboardPropertyName key, T value)
		{
			blackboard.SetStructValue(key, value);
		}
	}
}
