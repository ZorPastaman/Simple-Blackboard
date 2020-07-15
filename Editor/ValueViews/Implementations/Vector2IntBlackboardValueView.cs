// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector2IntBlackboardValueView : BlackboardValueView<Vector2Int>
	{
		public override Vector2Int DrawValue(string label, Vector2Int value)
		{
			return EditorGUILayout.Vector2IntField(label, value);
		}
	}
}
