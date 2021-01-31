// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class AnimationCurveBlackboardValueView : BlackboardValueView<AnimationCurve>
	{
		public override AnimationCurve DrawValue(string label, AnimationCurve value)
		{
			if (value == null)
			{
				value = new AnimationCurve();
			}

			return EditorGUILayout.CurveField(label, value);
		}
	}
}
