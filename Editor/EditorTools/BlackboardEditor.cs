// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <summary>
	/// Static helper for drawing a <see cref="Zor.SimpleBlackboard.Core.Blackboard"/> in the editor.
	/// </summary>
	public static class BlackboardEditor
	{
		private static readonly List<Type> s_tableTypes = new List<Type>();
		private static readonly List<Type> s_noEditorTables = new List<Type>();

		private static readonly GenericMenu.MenuFunction2 s_onCreatePopup = OnCreatePopup;

		/// <summary>
		/// Draws an editor for <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">An editor is drawn for this.</param>
		public static void DrawBlackboard(Blackboard blackboard)
		{
			if (blackboard == null)
			{
				return;
			}

			try
			{
				blackboard.GetValueTypes(s_tableTypes);
				s_tableTypes.Sort((left, right) => string.CompareOrdinal(left.Name, right.Name));
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

		private static void DrawTables(Blackboard blackboard)
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

			EditorGUILayout.PrefixLabel("Other Types");

			EditorGUILayout.BeginVertical();

			for (int i = 0; i < noEditorCount; ++i)
			{
				EditorGUILayout.LabelField(s_noEditorTables[i].Name);
			}

			EditorGUILayout.EndVertical();

			EditorGUILayout.EndHorizontal();
		}

		private static void DrawAdd(Blackboard blackboard)
		{
			EditorGUILayout.Separator();

			if (GUILayout.Button("Add"))
			{
				var menu = new GenericMenu();

				Vector2 position = Event.current.mousePosition;
				Vector2 screenPoint = GUIUtility.GUIToScreenPoint(new Vector2(position.x, position.y));

				BlackboardEditorToolsCollection.GetValueViewTypes(s_tableTypes);

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
