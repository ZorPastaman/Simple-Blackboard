// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class BoolBlackboardValueView : BlackboardValueView<bool>
	{
		public override bool DrawValue(string label, bool value)
		{
			return EditorGUILayout.Toggle(label, value);
		}
	}
}
