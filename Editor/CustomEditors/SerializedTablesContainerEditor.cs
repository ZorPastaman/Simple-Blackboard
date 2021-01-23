// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zor.SimpleBlackboard.EditorTools;
using Object = UnityEngine.Object;

namespace Zor.SimpleBlackboard.Serialization
{
	[CustomEditor(typeof(SimpleSerializedTablesContainer))]
	public sealed class SerializedTablesContainerEditor : Editor
	{
		private const string SerializedTablesPropertyName = "m_SerializedTables";

		private readonly Dictionary<Object, Editor> m_editors = new Dictionary<Object, Editor>();
		private readonly List<Type> m_tableTypes = new List<Type>();
		private readonly List<Type> m_valueTypes = new List<Type>();

		private GenericMenu.MenuFunction2 m_addTable;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			SerializedProperty serializedTables = serializedObject.FindProperty(SerializedTablesPropertyName);

			for (int i = 0, count = serializedTables.arraySize; i < count; ++i)
			{
				SerializedProperty serializedTable = serializedTables.GetArrayElementAtIndex(i);
				Object objectValue = serializedTable.objectReferenceValue;

				if (objectValue == null)
				{
					continue;
				}

				if (!m_editors.TryGetValue(objectValue, out Editor editor))
				{
					editor = CreateEditorWithContext(new[] {objectValue}, target);
					m_editors.Add(objectValue, editor);
				}

				EditorGUILayout.BeginVertical(GUI.skin.box);

				editor.OnInspectorGUI();

				if (GUILayout.Button("Remove"))
				{
					DestroyImmediate(objectValue, true);
					SerializedPropertyHelper.CompletelyRemove(serializedTables, i);

					i--;
					count = serializedTables.arraySize;

					serializedObject.ApplyModifiedProperties();
					AssetDatabase.SaveAssets();
					AssetDatabase.Refresh();
				}

				EditorGUILayout.EndVertical();
			}

			EditorGUILayout.Separator();

			if (GUILayout.Button("Add"))
			{
				MakeAddTableDropdown(serializedTables);
			}
		}

		private void Awake()
		{
			m_addTable = AddTable;
		}

		private void OnDestroy()
		{
			Dictionary<Object, Editor>.ValueCollection.Enumerator enumerator = m_editors.Values.GetEnumerator();

			while (enumerator.MoveNext())
			{
				DestroyImmediate(enumerator.Current);
			}

			enumerator.Dispose();
		}

		private void MakeAddTableDropdown(SerializedProperty serializedTables)
		{
			m_tableTypes.Clear();
			m_valueTypes.Clear();
			SerializedTableTypesCollection.GetSerializedTableTypes(m_tableTypes, m_valueTypes, serializedTables);

			var menu = new GenericMenu();
			for (int i = 0, count = m_tableTypes.Count; i < count; ++i)
			{
				Type tableType = m_tableTypes[i];
				Type valueType = m_valueTypes[i];
				menu.AddItem(new GUIContent(valueType.Name), false, m_addTable, tableType);
			}
			menu.ShowAsContext();
		}

		private void AddTable(object obj)
		{
			var type = (Type)obj;
			ScriptableObject instance = CreateInstance(type);
			instance.name = type.Name;

			AssetDatabase.AddObjectToAsset(instance, target);

			SerializedProperty serializedTables = serializedObject.FindProperty(SerializedTablesPropertyName);
			int index = serializedTables.arraySize++;
			serializedTables.GetArrayElementAtIndex(index).objectReferenceValue = instance;

			serializedObject.ApplyModifiedProperties();
			AssetDatabase.SaveAssets();
		}
	}
}
