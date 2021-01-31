// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class BoundsIntBlackboardValueView : BlackboardValueView<BoundsInt>
	{
		public override BoundsInt DrawValue(string label, BoundsInt value)
		{
			return EditorGUILayout.BoundsIntField(label, value);
		}
	}
}
