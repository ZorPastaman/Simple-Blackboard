// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class IntBlackboardValueView : BlackboardValueView<int>
	{
		public override int DrawValue(string label, int value)
		{
			return EditorGUILayout.IntField(label, value);
		}
	}
}
