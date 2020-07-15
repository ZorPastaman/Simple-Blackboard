// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zor.SimpleBlackboard.Serialization;
using Object = UnityEngine.Object;

namespace Zor.SimpleBlackboard.Components
{
	[CustomPropertyDrawer(typeof(BlackboardPropertyReference))]
	public sealed class BlackboardPropertyReferenceEditor : PropertyDrawer
	{
private const string BlackboardContainerPropertyName = "blackboardContainer";
		private const string PropertyNamePropertyName = "propertyName";

		private const string SerializedContainersPropertyName = "m_SerializedContainers";

		private static readonly GenericMenu.MenuFunction2 s_onMenuItem = OnMenuItem;

		private static readonly List<(string, Type)> s_propertyKeysCache = new List<(string, Type)>();

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = EditorGUI.GetPropertyHeight(property, false);

			if (property.isExpanded)
			{
				SerializedProperty blackboardContainerProperty =
					property.FindPropertyRelative(BlackboardContainerPropertyName);
				SerializedProperty propertyNameProperty = property.FindPropertyRelative(PropertyNamePropertyName);

				height += EditorGUI.GetPropertyHeight(blackboardContainerProperty)
					+ EditorGUI.GetPropertyHeight(propertyNameProperty) + 2f * EditorGUIUtility.standardVerticalSpacing;
			}

			return height;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = EditorGUI.GetPropertyHeight(property, false);
			EditorGUI.PropertyField(position, property, false);

			if (!property.isExpanded)
			{
				return;
			}

			++EditorGUI.indentLevel;

			SerializedProperty blackboardContainerProperty =
				property.FindPropertyRelative(BlackboardContainerPropertyName);
			SerializedProperty propertyNameProperty = property.FindPropertyRelative(PropertyNamePropertyName);
			Object blackboardContainer = blackboardContainerProperty.objectReferenceValue;
			SerializedProperty serializedContainersProperty = blackboardContainer == null
				? null
				: new SerializedObject(blackboardContainer).FindProperty(SerializedContainersPropertyName);

			position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
			position.height = EditorGUI.GetPropertyHeight(blackboardContainerProperty);
			EditorGUI.PropertyField(position, blackboardContainerProperty, true);

			position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
			position.height = EditorGUI.GetPropertyHeight(propertyNameProperty);
			position.width -= position.height;
			EditorGUI.PropertyField(position, propertyNameProperty, true);

			position.x += position.width;
			position.width = position.height;

			bool wasEnabled = GUI.enabled;
			GUI.enabled = blackboardContainerProperty.objectReferenceValue != null;
			if (EditorGUI.DropdownButton(position, GUIContent.none, FocusType.Keyboard))
			{
				try
				{
					FillPropertyNamesCache(serializedContainersProperty);
					CreateGenericMenu(propertyNameProperty);
				}
				finally
				{
					s_propertyKeysCache.Clear();
				}
			}
			GUI.enabled = wasEnabled;

			--EditorGUI.indentLevel;
		}

		private static void FillPropertyNamesCache(SerializedProperty serializedContainersProperty)
		{
			if (serializedContainersProperty == null)
			{
				return;
			}

			for (int i = 0, count = serializedContainersProperty.arraySize; i < count; ++i)
			{
				SerializedProperty serializedContainerProperty = serializedContainersProperty.GetArrayElementAtIndex(i);
				var serializedContainer = serializedContainerProperty.objectReferenceValue as SerializedContainer;

				if (serializedContainer == null)
				{
					continue;
				}

				serializedContainer.GetKeys(s_propertyKeysCache);
			}
		}

		private static void CreateGenericMenu(SerializedProperty propertyNameProperty)
		{
			var menu = new GenericMenu();

			for (int i = 0, count = s_propertyKeysCache.Count; i < count; ++i)
			{
				(string propertyKey, Type type) = s_propertyKeysCache[i];
				var menuItem = new MenuItem(propertyNameProperty, propertyKey);
				menu.AddItem(new GUIContent($"{propertyKey} : {type.Name}"),
					false, s_onMenuItem, menuItem);
			}

			menu.ShowAsContext();
		}

		private static void OnMenuItem(object menuItemObject)
		{
			var menuItem = (MenuItem)menuItemObject;
			menuItem.propertyNameProperty.stringValue = menuItem.propertyKeyValue;
			menuItem.propertyNameProperty.serializedObject.ApplyModifiedProperties();
		}

		private readonly struct MenuItem
		{
			public readonly SerializedProperty propertyNameProperty;
			public readonly string propertyKeyValue;

			public MenuItem(SerializedProperty propertyNameProperty, string propertyKeyValue)
			{
				this.propertyNameProperty = propertyNameProperty;
				this.propertyKeyValue = propertyKeyValue;
			}
		}
	}
}
