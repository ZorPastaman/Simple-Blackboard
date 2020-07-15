// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class SbyteBlackboardValueView : BlackboardValueView<sbyte>
	{
		public override sbyte DrawValue(string label, sbyte value)
		{
			return (sbyte)Mathf.Clamp(EditorGUILayout.IntField(label, value), sbyte.MinValue, sbyte.MaxValue);
		}
	}
}
