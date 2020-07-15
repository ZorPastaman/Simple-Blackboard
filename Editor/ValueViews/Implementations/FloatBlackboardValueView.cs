// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class FloatBlackboardValueView : BlackboardValueView<float>
	{
		public override float DrawValue(string label, float value)
		{
			return EditorGUILayout.FloatField(label, value);
		}
	}
}
