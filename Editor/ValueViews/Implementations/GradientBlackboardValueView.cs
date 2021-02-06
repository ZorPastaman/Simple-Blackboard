// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

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
			if (value == null)
			{
				value = new Gradient();
			}

			return EditorGUILayout.GradientField(label, value);
		}
	}
}
