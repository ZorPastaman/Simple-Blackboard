// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class CharBlackboardValueView : BlackboardValueView<char, char, CharField>
	{
		public override CharField CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var charField = new CharField(label);

			if (blackboardRoot != null)
			{
				charField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), charField.value);
					}
				});
			}

			return charField;
		}

		public override void UpdateValue(VisualElement visualElement, char value)
		{
			var charField = (CharField)visualElement;
			charField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var charField = (CharField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), charField.value);
		}

		public override char DrawValue(string label, char value)
		{
			string result = EditorGUILayout.TextField(label, value.ToString());
			return !string.IsNullOrEmpty(result) ? result[0] : default;
		}
	}
}
