// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class FloatBlackboardValueView : BlackboardValueView<float>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new FloatField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var floatField = (FloatField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), floatField.value);
		}

		public override float DrawValue(string label, float value)
		{
			return EditorGUILayout.FloatField(label, value);
		}
	}
}
