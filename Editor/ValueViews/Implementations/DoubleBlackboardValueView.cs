// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class DoubleBlackboardValueView : BlackboardValueView<double>
	{
		public override BaseField<double> CreateBaseField(string label)
		{
			return new DoubleField(label);
		}

		public override double DrawValue(string label, double value)
		{
			return EditorGUILayout.DoubleField(label, value);
		}
	}
}
