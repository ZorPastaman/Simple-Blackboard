// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.Helpers
{
	internal static class UIElementsHelper
	{
		[NotNull]
		public static VisualElement CreateDefaultObjectGUI([NotNull] SerializedObject serializedObject)
		{
			var root = new VisualElement();
			SerializedProperty iterator = serializedObject.GetIterator();

			if (iterator.NextVisible(true))
			{
				do
				{
					var propertyField = new PropertyField(iterator) {name = iterator.propertyPath};
					propertyField.SetEnabled(iterator.editable & iterator.name != "m_Script");
					root.Add(propertyField);
				} while (iterator.NextVisible(false));
			}

			return root;
		}
	}
}
