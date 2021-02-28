// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class RectIntBlackboardValueView : BlackboardValueView<RectInt>
	{
		public override BaseField<RectInt> CreateBaseField(string label)
		{
			return new RectIntField(label);
		}

		public override RectInt DrawValue(string label, RectInt value)
		{
			return EditorGUILayout.RectIntField(label, value);
		}
	}
}
