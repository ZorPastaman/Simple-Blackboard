// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class BoolBlackboardValueView : BlackboardValueView<bool>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new Toggle(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var toggle = (Toggle)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), toggle.value);
		}

		public override bool DrawValue(string label, bool value)
		{
			return EditorGUILayout.Toggle(label, value);
		}
	}
}
