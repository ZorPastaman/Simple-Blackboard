// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class StringBlackboardValueView : BlackboardValueView<string>
	{
		public override string DrawValue(string label, string value)
		{
			return EditorGUILayout.TextField(label, value);
		}
	}
}
