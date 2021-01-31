// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class ByteBlackboardValueView : BlackboardValueView<byte>
	{
		public override byte DrawValue(string label, byte value)
		{
			return (byte)Mathf.Clamp(EditorGUILayout.IntField(label, value), byte.MinValue, byte.MaxValue);
		}
	}
}
