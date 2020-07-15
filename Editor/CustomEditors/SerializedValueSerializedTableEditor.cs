// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Zor.SimpleBlackboard.Debugging;

namespace Zor.SimpleBlackboard.Serialization
{
	[CustomEditor(typeof(SerializedValueSerializedTable_Base), true)]
	public sealed class SerializedValueSerializedTableEditor : Editor
	{
		private const string KeysPropertyName = "m_Keys";
		private const string ValuesPropertyName = "m_Values";

		private SerializedProperty m_keysProperty;
		private SerializedProperty m_valuesProperty;
		private ReorderableList m_list;

		private bool m_correctType;

		public override void OnInspectorGUI()
		{
			if (!m_correctType)
			{
				base.OnInspectorGUI();
				return;
			}

			m_list.DoLayoutList();
		}

		private void OnEnable()
		{
			Type targetType = target.GetType();

			if (!IsSubclassOfRightClass(targetType))
			{
				BlackboardDebug.LogError($"{targetType.FullName} is derived from {typeof(SerializedValueSerializedTable_Base).FullName}. Instead it should be derived from {typeof(StructSerializedValueSerializedTable<>).FullName} or {typeof(ClassSerializedValueSerializedTable<>).FullName}");
				return;
			}

			m_correctType = true;

			m_keysProperty = serializedObject.FindProperty(KeysPropertyName);
			m_valuesProperty = serializedObject.FindProperty(ValuesPropertyName);
			EnsureCapacity();

			m_list = new ReorderableList(serializedObject, m_keysProperty, false, true, true, true)
			{
				drawHeaderCallback = OnDrawHeader,
				elementHeightCallback = OnElementHeight,
				drawElementCallback = OnDrawElement,
				onAddCallback = OnAdd,
				onRemoveCallback = OnRemove
			};
		}

		private void OnDrawHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, $"{((SerializedTable_Base)target).valueType.Name} ({m_keysProperty.arraySize})");
		}

		private float OnElementHeight(int index)
		{
			SerializedProperty key = m_keysProperty.GetArrayElementAtIndex(index);
			SerializedProperty value = m_valuesProperty.GetArrayElementAtIndex(index);

			return EditorGUI.GetPropertyHeight(key, new GUIContent("Key"), true)
				+ EditorGUI.GetPropertyHeight(value, new GUIContent("Value"), true)
				+ EditorGUIUtility.standardVerticalSpacing * 2f;
		}

		private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty key = m_keysProperty.GetArrayElementAtIndex(index);
			SerializedProperty value = m_valuesProperty.GetArrayElementAtIndex(index);

			rect.x += 10f;
			rect.width -= 10f;
			rect.height = EditorGUI.GetPropertyHeight(key, new GUIContent("Key"),true);
			EditorGUI.PropertyField(rect, key, new GUIContent("Key"),true);

			rect.y += rect.height + EditorGUIUtility.standardVerticalSpacing;
			rect.height = EditorGUI.GetPropertyHeight(value, new GUIContent("Value"),true);
			EditorGUI.PropertyField(rect, value, new GUIContent("Value"),true);

			serializedObject.ApplyModifiedProperties();
		}

		private void OnAdd(ReorderableList list)
		{
			m_keysProperty.arraySize++;
			m_valuesProperty.arraySize++;

			serializedObject.ApplyModifiedProperties();
		}

		private void OnRemove(ReorderableList list)
		{
			int index = list.index;
			SerializedPropertyHelper.CompletelyRemove(m_keysProperty, index);
			SerializedPropertyHelper.CompletelyRemove(m_valuesProperty, index);

			serializedObject.ApplyModifiedProperties();
		}

		private void EnsureCapacity()
		{
			int capacity = Mathf.Min(m_keysProperty.arraySize, m_valuesProperty.arraySize);
			m_keysProperty.arraySize = capacity;
			m_valuesProperty.arraySize = capacity;
		}

		private static bool IsSubclassOfRightClass(Type toCheck)
		{
			while (toCheck != null && toCheck != typeof(object))
			{
				Type type = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;

				if (typeof(StructSerializedValueSerializedTable<>) == type ||
					typeof(ClassSerializedValueSerializedTable<>) == type)
				{
					return true;
				}

				toCheck = toCheck.BaseType;
			}

			return false;
		}
	}
}
