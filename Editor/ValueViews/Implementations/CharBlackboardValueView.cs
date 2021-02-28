// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class CharBlackboardValueView : BlackboardValueView<char>
	{
		public override BaseField<char> CreateBaseField(string label)
		{
			return new CharField(label);
		}

		public override char DrawValue(string label, char value)
		{
			string result = EditorGUILayout.TextField(label, value.ToString());
			return !string.IsNullOrEmpty(result) ? result[0] : default;
		}
	}
}
