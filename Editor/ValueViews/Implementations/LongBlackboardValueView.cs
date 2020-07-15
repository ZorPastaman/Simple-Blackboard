// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class LongBlackboardValueView : BlackboardValueView<long>
	{
		public override long DrawValue(string label, long value)
		{
			return EditorGUILayout.LongField(label, value);
		}
	}
}
