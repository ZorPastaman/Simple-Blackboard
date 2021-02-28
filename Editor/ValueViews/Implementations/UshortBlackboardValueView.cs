// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UshortBlackboardValueView : BlackboardValueView<ushort>
	{
		public override BaseField<ushort> CreateBaseField(string label)
		{
			return new UshortField(label);
		}

		public override ushort DrawValue(string label, ushort value)
		{
			return (ushort)Mathf.Clamp(EditorGUILayout.IntField(label, value), ushort.MinValue, ushort.MaxValue);
		}
	}
}
