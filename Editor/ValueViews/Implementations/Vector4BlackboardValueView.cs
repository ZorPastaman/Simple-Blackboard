// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector4BlackboardValueView : BlackboardValueView<Vector4>
	{
		public override Vector4 DrawValue(string label, Vector4 value)
		{
			return EditorGUILayout.Vector4Field(label, value);
		}
	}
}
