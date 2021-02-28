// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UintBlackboardValueView : BlackboardValueView<uint>
	{
		public override BaseField<uint> CreateBaseField(string label)
		{
			return new UintField(label);
		}

		public override uint DrawValue(string label, uint value)
		{
			return (uint)Math.Max(Math.Min(EditorGUILayout.LongField(label, value), uint.MaxValue), uint.MinValue);
		}
	}
}
