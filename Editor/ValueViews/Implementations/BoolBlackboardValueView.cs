// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class BoolBlackboardValueView : BlackboardValueView<bool, bool, Toggle>
	{
		public override Toggle CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var toggle = new Toggle(label);

			if (blackboardRoot != null)
			{
				toggle.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), toggle.value);
					}
				});
			}

			return toggle;
		}

		public override void UpdateValue(VisualElement visualElement, bool value)
		{
			var toggle = (Toggle)visualElement;
			toggle.value = value;
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
