// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UintBlackboardValueView : BlackboardValueView<uint>
	{
		public override uint DrawValue(string label, uint value)
		{
			return (uint)Mathf.Clamp(EditorGUILayout.IntField(label, (int)value), uint.MinValue, uint.MaxValue);
		}
	}
}
