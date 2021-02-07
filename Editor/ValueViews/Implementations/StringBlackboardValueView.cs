// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class StringBlackboardValueView : BlackboardValueView<string>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new TextField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var textField = (TextField)visualElement;
			blackboard.SetClassValue(new BlackboardPropertyName(key), textField.value);
		}

		public override string DrawValue(string label, string value)
		{
			return EditorGUILayout.TextField(label, value);
		}
	}
}
