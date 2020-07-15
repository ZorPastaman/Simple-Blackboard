// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class DoubleBlackboardValueView : BlackboardValueView<double>
	{
		public override double DrawValue(string label, double value)
		{
			return EditorGUILayout.DoubleField(label, value);
		}
	}
}
