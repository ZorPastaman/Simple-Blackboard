// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class GradientBlackboardValueView : BlackboardValueView<Gradient>
	{
		public override Gradient DrawValue(string label, Gradient value)
		{
			return EditorGUILayout.GradientField(label, value);
		}
	}
}
