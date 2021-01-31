// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector3BlackboardValueView : BlackboardValueView<Vector3>
	{
		public override Vector3 DrawValue(string label, Vector3 value)
		{
			return EditorGUILayout.Vector3Field(label, value);
		}
	}
}
