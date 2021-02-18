// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <summary>
	/// Static helper for drawing a <see cref="Zor.SimpleBlackboard.Core.Blackboard"/> in the editor.
	/// </summary>
	public static class BlackboardEditor
	{
		private const string TablesElementName = "Tables";
		private const string NoEditorContainerElementName = "NoEditorContainer";
		private const string AddElementName = "Add";

		private const string OtherTypesLabel = "Other Types";
		private const string AddButtonLabel = "Add";

		private static readonly Comparison<Type> s_typeByNameComparison = (left, right) =>
			string.CompareOrdinal(left.Name, right.Name);
		private static readonly Comparison<VisualElement> s_visualElementByTypeNameComparison = (left, right) =>
		{
			if (left.userData is Type leftType && right.userData is Type rightType)
			{
				return s_typeByNameComparison(leftType, rightType);
			}

			if (!(left.userData is Type) && !(right.userData is Type))
			{
				return 0;
			}

			return left.userData is Type ? 1 : -1;
		};

		private static readonly List<Type> s_tableTypes = new List<Type>();
		private static readonly List<Type> s_noEditorTables = new List<Type>();

		private static readonly GenericMenu.MenuFunction2 s_onCreatePopup = OnCreatePopup;

		/// <summary>
		/// Draws an editor for <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">An editor is drawn for this.</param>
		public static void DrawBlackboard([CanBeNull] Blackboard blackboard)
		{
			if (blackboard == null)
			{
				return;
			}

			lock (blackboard)
			{
				try
				{
					blackboard.GetValueTypes(s_tableTypes);
					s_tableTypes.Sort(s_typeByNameComparison);
					DrawTables(blackboard);
					s_tableTypes.Clear();
					DrawNoEditor();
					DrawAdd(blackboard);
				}
				finally
				{
					s_tableTypes.Clear();
					s_noEditorTables.Clear();
				}
			}
		}

		[NotNull]
		public static VisualElement CreateBlackboardVisualElement()
		{
			var root = new VisualElement();
			root.style.display = DisplayStyle.None;

			var tables = new VisualElement {name = TablesElementName};
			root.Add(tables);

			var noEditor = new VisualElement();
			noEditor.style.flexDirection = FlexDirection.Row;
			var noEditorLabel = new Label(OtherTypesLabel);
			var noEditorContainer = new VisualElement {name = NoEditorContainerElementName};
			noEditor.Add(noEditorLabel);
			noEditor.Add(noEditorContainer);
			root.Add(noEditor);

			var addButton = new Button(() =>
			{
				if (!(root.userData is Blackboard blackboard))
				{
					return;
				}

				var menu = new GenericMenu();

				Vector2 position = Event.current.mousePosition;
				Vector2 screenPoint = GUIUtility.GUIToScreenPoint(new Vector2(position.x, position.y));

				BlackboardEditorToolsCollection.GetValueViewTypes(s_tableTypes);
				s_tableTypes.Sort(s_typeByNameComparison);

				for (int i = 0, count = s_tableTypes.Count; i < count; ++i)
				{
					Type type = s_tableTypes[i];

					var popupInfo = new PopupInfo
					{
						type = type,
						blackboard = blackboard,
						screenPoint = screenPoint,
					};

					menu.AddItem(new GUIContent(type.Name), false, s_onCreatePopup, popupInfo);
				}

				s_tableTypes.Clear();
				menu.ShowAsContext();
			}) {name = AddElementName, text = AddButtonLabel};
			root.Add(addButton);

			return root;
		}

		public static void UpdateBlackboardVisualElement([NotNull] VisualElement blackboardVisualElement,
			[CanBeNull] Blackboard blackboard)
		{
			blackboardVisualElement.userData = blackboard;

			if (blackboard == null)
			{
				blackboardVisualElement.style.display = DisplayStyle.None;
				return;
			}

			blackboardVisualElement.style.display = DisplayStyle.Flex;

			lock (blackboard)
			{
				try
				{
					blackboard.GetValueTypes(s_tableTypes);
					VisualElement tables = blackboardVisualElement.Q(TablesElementName);
					UpdateTables(tables, blackboardVisualElement, blackboard);
					VisualElement noEditor =
						blackboardVisualElement.Q(NoEditorContainerElementName);
					UpdateNoEditor(noEditor);
				}
				finally
				{
					s_tableTypes.Clear();
					s_noEditorTables.Clear();
				}
			}
		}

		private static void DrawTables([NotNull] Blackboard blackboard)
		{
			EditorGUI.BeginChangeCheck();

			for (int i = 0, count = s_tableTypes.Count; i < count; ++i)
			{
				Type type = s_tableTypes[i];

				if (!BlackboardEditorToolsCollection.TryGetTableEditor(type, out BlackboardTableEditor_Base editor))
				{
					s_noEditorTables.Add(type);
					continue;
				}

				EditorGUILayout.BeginVertical(GUI.skin.box);
				editor.Draw(blackboard);
				EditorGUILayout.EndVertical();
			}
		}

		private static void DrawNoEditor()
		{
			int noEditorCount = s_noEditorTables.Count;

			if (noEditorCount == 0)
			{
				return;
			}

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.PrefixLabel(OtherTypesLabel);

			EditorGUILayout.BeginVertical();

			for (int i = 0; i < noEditorCount; ++i)
			{
				EditorGUILayout.LabelField(s_noEditorTables[i].Name);
			}

			EditorGUILayout.EndVertical();

			EditorGUILayout.EndHorizontal();
		}

		private static void DrawAdd([NotNull] Blackboard blackboard)
		{
			EditorGUILayout.Separator();

			if (GUILayout.Button(AddButtonLabel))
			{
				var menu = new GenericMenu();

				Vector2 position = Event.current.mousePosition;
				Vector2 screenPoint = GUIUtility.GUIToScreenPoint(new Vector2(position.x, position.y));

				BlackboardEditorToolsCollection.GetValueViewTypes(s_tableTypes);
				s_tableTypes.Sort(s_typeByNameComparison);

				for (int i = 0, count = s_tableTypes.Count; i < count; ++i)
				{
					Type type = s_tableTypes[i];

					var popupInfo = new PopupInfo
					{
						type = type,
						blackboard = blackboard,
						screenPoint = screenPoint,
					};

					menu.AddItem(new GUIContent(type.Name), false, s_onCreatePopup, popupInfo);
				}

				menu.ShowAsContext();
			}
		}

		private static void UpdateTables([NotNull] VisualElement root, [NotNull] VisualElement blackboardRoot,
			[NotNull] Blackboard blackboard)
		{
			for (int i = root.childCount - 1; i >= 0; --i)
			{
				VisualElement element = root[i];

				if (element.userData is Type type && !s_tableTypes.Contains(type))
				{
					root.RemoveAt(i);
				}
			}

			for (int i = 0, count = s_tableTypes.Count; i < count; ++i)
			{
				Type type = s_tableTypes[i];

				if (!BlackboardEditorToolsCollection.TryGetTableEditor(type, out BlackboardTableEditor_Base editor))
				{
					s_noEditorTables.Add(type);
					continue;
				}

				VisualElement table = FindElementWithType(root, type);

				if (table == null)
				{
					table = editor.CreateTable();
					table.userData = type;
					root.Add(table);
				}

				editor.UpdateTable(table, blackboardRoot, blackboard);
			}

			root.Sort(s_visualElementByTypeNameComparison);
		}

		private static void UpdateNoEditor([NotNull] VisualElement root)
		{
			int noEditorCount = s_noEditorTables.Count;

			if (noEditorCount == 0)
			{
				root.style.display = DisplayStyle.None;
				return;
			}

			root.style.display = DisplayStyle.Flex;

			for (int i = root.childCount - 1; i >= 0; --i)
			{
				VisualElement element = root[i];

				if (element.userData is Type type && !s_noEditorTables.Contains(type))
				{
					root.ElementAt(i);
				}
			}

			for (int i = 0; i < noEditorCount; ++i)
			{
				Type type = s_noEditorTables[i];

				if (FindElementWithType(root, type) == null)
				{
					var element = new Label(type.Name) {userData = type};
					root.Add(element);
				}
			}

			root.Sort(s_visualElementByTypeNameComparison);
		}

		[CanBeNull]
		private static VisualElement FindElementWithType([NotNull] VisualElement root, [NotNull] Type type)
		{
			for (int i = 0, count = root.childCount; i < count; ++i)
			{
				VisualElement element = root[i];

				if (element.userData is Type containedType && containedType == type)
				{
					return element;
				}
			}

			return null;
		}

		private static void OnCreatePopup(object popupInfoObject)
		{
			var popupInfo = (PopupInfo)popupInfoObject;

			BlackboardEditorToolsCollection.CreateAddPopup(popupInfo.type, popupInfo.blackboard, popupInfo.type.Name,
				popupInfo.screenPoint);
		}

		private class PopupInfo
		{
			public Type type;
			public Blackboard blackboard;
			public Vector2 screenPoint;
		}
	}
}
