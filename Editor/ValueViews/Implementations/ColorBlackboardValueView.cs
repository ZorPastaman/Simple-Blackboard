// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class ColorBlackboardValueView : BlackboardValueView<Color>
	{
		public override Color DrawValue(string label, Color value)
		{
			return EditorGUILayout.ColorField(label, value);
		}
	}
}
