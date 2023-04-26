// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <summary>
	/// Static helper for drawing a <see cref="Blackboard"/> in the editor.
	/// </summary>
	public static class BlackboardEditor
	{
		[NotNull] private const string TablesElementName = "Tables";
		[NotNull] private const string NoEditorElementName = "NoEditor";
		[NotNull] private const string NoEditorContainerElementName = "NoEditorContainer";
		[NotNull] private const string AddElementName = "Add";

		[NotNull] private const string OtherTypesLabel = "Other Types";
		[NotNull] private const string AddButtonLabel = "Add";

		[NotNull] private static readonly Comparison<Type> s_typeByNameComparison = (left, right) =>
			string.CompareOrdinal(left.Name, right.Name);
		[NotNull]
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

		[NotNull]
		private static readonly EventCallback<PointerDownEvent, ToolbarMenu> s_onToolbarPointerDown = (c, menu) =>
		{
			menu.userData = GUIUtility.GUIToScreenPoint(c.originalMousePosition);
		};
		[NotNull] private static readonly Action<DropdownMenuAction> s_onDropdownAction = a =>
		{
			var popupInfo = (UIElementsPopupInfo)a.userData;
			if (popupInfo.blackboardRoot.userData is Blackboard blackboard)
			{
				Vector2 position = popupInfo.menuButton.userData is Vector2 pos
					? pos
					: popupInfo.menuButton.LocalToWorld(Vector2.zero);
				BlackboardEditorToolsCollection.CreateAddPopup(popupInfo.type, blackboard,
					popupInfo.type.Name, position);
			}
		};

		[NotNull] private static readonly GenericMenu.MenuFunction2 s_onCreatePopup = OnCreatePopup;

		[NotNull] private static readonly List<Type> s_tableTypes = new List<Type>();
		[NotNull] private static readonly List<Type> s_noEditorTables = new List<Type>();

		/// <summary>
		/// Draws an editor for the <paramref name="blackboard"/>.
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

		/// <summary>
		/// Creates a <see cref="VisualElement"/> for a <see cref="Blackboard"/>.
		/// </summary>
		/// <returns>View template for a <see cref="Blackboard"/>.</returns>
		/// <remarks>
		/// <para>
		/// The returned <see cref="VisualElement"/> is used in <see cref="UpdateBlackboardVisualElement"/>.
		/// </para>
		/// <para>Do not modify the returned <see cref="VisualElement"/>.</para>
		/// </remarks>
		[NotNull, Pure]
		public static VisualElement CreateBlackboardVisualElement()
		{
			var blackboardRoot = new VisualElement();
			blackboardRoot.style.display = DisplayStyle.None;

			var tables = new VisualElement {name = TablesElementName};
			blackboardRoot.Add(tables);

			var noEditor = new VisualElement {name = NoEditorElementName};
			noEditor.style.flexDirection = FlexDirection.Row;
			var noEditorLabel = new Label(OtherTypesLabel);
			IStyle noEditorLabelStyle = noEditorLabel.style;
			noEditorLabelStyle.width = 140f;
			noEditorLabelStyle.unityFontStyleAndWeight = FontStyle.Bold;
			var noEditorContainer = new VisualElement {name = NoEditorContainerElementName};
			noEditor.Add(noEditorLabel);
			noEditor.Add(noEditorContainer);
			blackboardRoot.Add(noEditor);

			var toolbar = new Toolbar();
			blackboardRoot.Add(toolbar);

			try
			{
				BlackboardEditorToolsCollection.GetValueViewTypes(s_tableTypes);
				s_tableTypes.Sort(s_typeByNameComparison);

				var toolbarMenu = new ToolbarMenu {name = AddElementName, text = AddButtonLabel};
				toolbarMenu.RegisterCallback(s_onToolbarPointerDown, toolbarMenu);
				DropdownMenu menu = toolbarMenu.menu;

				for (int i = 0, count = s_tableTypes.Count; i < count; ++i)
				{
					Type type = s_tableTypes[i];
					var popupInfo = new UIElementsPopupInfo(blackboardRoot, toolbarMenu, type);
					menu.AppendAction(type.Name, s_onDropdownAction, a => DropdownMenuAction.Status.Normal, popupInfo);
				}

				toolbar.Add(toolbarMenu);
			}
			finally
			{
				s_tableTypes.Clear();
			}

			return blackboardRoot;
		}

		/// <summary>
		/// Updates the <paramref name="blackboardVisualElement"/> with the <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboardVisualElement">View template for a <see cref="Blackboard"/>.</param>
		/// <param name="blackboard">Blackboard used to update the view.</param>
		/// <remarks>
		/// <paramref name="blackboardVisualElement"/> must be created in <see cref="CreateBlackboardVisualElement"/>.
		/// </remarks>
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
					UpdateTables(tables, blackboardVisualElement);
					VisualElement noEditor = blackboardVisualElement.Q(NoEditorElementName);
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

				editor.Draw(blackboard);
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
					var popupInfo = new PopupInfo (type, blackboard, screenPoint);
					menu.AddItem(new GUIContent(type.Name), false, s_onCreatePopup, popupInfo);
				}

				menu.ShowAsContext();
			}
		}

		private static void UpdateTables([NotNull] VisualElement tablesRoot, [NotNull] VisualElement blackboardRoot)
		{
			bool structureChanged = false;

			for (int i = tablesRoot.childCount - 1; i >= 0; --i)
			{
				VisualElement element = tablesRoot[i];

				if (element.userData is Type type && !s_tableTypes.Contains(type))
				{
					tablesRoot.RemoveAt(i);
					structureChanged = true;
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

				VisualElement tableRoot = GetElementInChildrenOfType(tablesRoot, type);

				if (tableRoot == null)
				{
					tableRoot = editor.CreateTable();
					tableRoot.userData = type;
					tablesRoot.Add(tableRoot);
					structureChanged = true;
				}

				editor.UpdateTable(tableRoot, blackboardRoot);
			}

			if (structureChanged)
			{
				tablesRoot.Sort(s_visualElementByTypeNameComparison);
			}
		}

		private static void UpdateNoEditor([NotNull] VisualElement noEditorRoot)
		{
			int noEditorCount = s_noEditorTables.Count;

			if (noEditorCount == 0)
			{
				noEditorRoot.style.display = DisplayStyle.None;
				return;
			}

			noEditorRoot.style.display = DisplayStyle.Flex;

			VisualElement container = noEditorRoot.Q(NoEditorContainerElementName);
			bool structureChanged = false;

			for (int i = container.childCount - 1; i >= 0; --i)
			{
				VisualElement element = container[i];

				if (element.userData is Type type && !s_noEditorTables.Contains(type))
				{
					container.RemoveAt(i);
					structureChanged = true;
				}
			}

			for (int i = 0; i < noEditorCount; ++i)
			{
				Type type = s_noEditorTables[i];

				if (GetElementInChildrenOfType(container, type) == null)
				{
					var element = new Label(type.Name) {userData = type};
					container.Add(element);
					structureChanged = true;
				}
			}

			if (structureChanged)
			{
				container.Sort(s_visualElementByTypeNameComparison);
			}
		}

		[CanBeNull, Pure]
		private static VisualElement GetElementInChildrenOfType([NotNull] VisualElement root, [NotNull] Type type)
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

		private static void OnCreatePopup([NotNull] object popupInfoObject)
		{
			var popupInfo = (PopupInfo)popupInfoObject;

			BlackboardEditorToolsCollection.CreateAddPopup(popupInfo.type, popupInfo.blackboard, popupInfo.type.Name,
				popupInfo.screenPoint);
		}

		private readonly struct PopupInfo
		{
			[NotNull] public readonly Type type;
			[NotNull] public readonly Blackboard blackboard;
			public readonly Vector2 screenPoint;

			public PopupInfo([NotNull] Type type, [NotNull] Blackboard blackboard, Vector2 screenPoint)
			{
				this.type = type;
				this.blackboard = blackboard;
				this.screenPoint = screenPoint;
			}
		}

		private readonly struct UIElementsPopupInfo
		{
			[NotNull] public readonly VisualElement blackboardRoot;
			[NotNull] public readonly VisualElement menuButton;
			[NotNull] public readonly Type type;

			public UIElementsPopupInfo([NotNull] VisualElement blackboardRoot, [NotNull] VisualElement menuButton,
				[NotNull] Type type)
			{
				this.blackboardRoot = blackboardRoot;
				this.menuButton = menuButton;
				this.type = type;
			}
		}
	}
}
