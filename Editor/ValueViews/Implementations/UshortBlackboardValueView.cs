// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UshortBlackboardValueView : BlackboardValueView<ushort>
	{
		public override ushort DrawValue(string label, ushort value)
		{
			long result = EditorGUILayout.LongField(label, value);

			if (result < ushort.MinValue)
			{
				result = ushort.MinValue;
			}
			else if (result > ushort.MaxValue)
			{
				result = ushort.MaxValue;
			}

			return (ushort)result;
		}
	}
}
