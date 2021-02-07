// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class IntBlackboardValueView : BlackboardValueView<int>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new IntegerField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var intField = (IntegerField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), intField.value);
		}

		public override int DrawValue(string label, int value)
		{
			return EditorGUILayout.IntField(label, value);
		}
	}
}
