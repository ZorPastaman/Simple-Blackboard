// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class ShortBlackboardValueView : BlackboardValueView<short>
	{
		public override short DrawValue(string label, short value)
		{
			return (short)Mathf.Clamp(EditorGUILayout.IntField(label, value), short.MinValue, short.MaxValue);
		}
	}
}
