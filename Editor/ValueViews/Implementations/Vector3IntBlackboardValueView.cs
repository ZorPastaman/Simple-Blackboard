// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector3IntBlackboardValueView : BlackboardValueView<Vector3Int>
	{
		public override Vector3Int DrawValue(string label, Vector3Int value)
		{
			return EditorGUILayout.Vector3IntField(label, value);
		}
	}
}
