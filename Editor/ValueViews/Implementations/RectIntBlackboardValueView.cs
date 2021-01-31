// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class RectIntBlackboardValueView : BlackboardValueView<RectInt>
	{
		public override RectInt DrawValue(string label, RectInt value)
		{
			return EditorGUILayout.RectIntField(label, value);
		}
	}
}
