// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class LongBlackboardValueView : BlackboardValueView<long>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new LongField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var longField = (LongField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), longField.value);
		}

		public override long DrawValue(string label, long value)
		{
			return EditorGUILayout.LongField(label, value);
		}
	}
}
