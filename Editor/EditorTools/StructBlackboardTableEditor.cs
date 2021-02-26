// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Collections.Generic;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.BlackboardValueViews;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <inheritdoc/>
	internal sealed class StructBlackboardTableEditor<TValue, TBaseValue, TBaseField> : BlackboardTableEditor<TValue, TBaseValue, TBaseField>
		where TValue : struct, TBaseValue
		where TBaseField : BaseField<TBaseValue>
	{
		public StructBlackboardTableEditor(BlackboardValueView<TValue, TBaseValue, TBaseField> blackboardValueView) : base(blackboardValueView)
		{
		}

		/// <inheritdoc/>
		protected override void GetProperties(Blackboard blackboard,
			List<KeyValuePair<BlackboardPropertyName, TValue>> properties)
		{
			blackboard.GetStructProperties(properties);
		}

		/// <inheritdoc/>
		protected override void SetValue(Blackboard blackboard, BlackboardPropertyName key, TValue value)
		{
			blackboard.SetStructValue(key, value);
		}
	}
}
