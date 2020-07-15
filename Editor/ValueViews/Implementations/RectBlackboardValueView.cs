// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class RectBlackboardValueView : BlackboardValueView<Rect>
	{
		public override Rect DrawValue(string label, Rect value)
		{
			return EditorGUILayout.RectField(label, value);
		}
	}
}
