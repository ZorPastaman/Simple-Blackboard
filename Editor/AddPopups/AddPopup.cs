// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor;
using UnityEngine;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardTableEditors
{
	/// <summary>
	/// Popup used for adding new properties to <see cref="Zor.SimpleBlackboard.Core.Blackboard"/> in the editor.
	/// </summary>
	internal sealed class AddPopup : EditorWindow
	{
		private GUIContent m_closeButtonIcon;
		private GUILayoutOption[] m_closeButtonOptions;

		private Blackboard m_blackboard;
		private string m_key;
		private IAddPopupValue m_addPopupValue;

		private Vector2 m_scrollPos;

		/// <summary>
		/// Sets necessary parameters to <see cref="AddPopup"/>. Call this before first <see cref="OnGUI"/>.
		/// </summary>
		/// <param name="blackboard">New property is added to this.</param>
		/// <param name="key">Initial key. It can be changed in <see cref="OnGUI"/>.</param>
		/// <param name="addPopupValue">Value wrapper.</param>
		/// <param name="popupPosition">Position of the popup in the editor space.</param>
		public void Setup(Blackboard blackboard, string key, IAddPopupValue addPopupValue, Vector2 popupPosition)
		{
			m_blackboard = blackboard;
			m_key = key;
			m_addPopupValue = addPopupValue;
			var size = new Vector2(450f, EditorGUIUtility.singleLineHeight * 8f
				+ EditorGUIUtility.standardVerticalSpacing * 6f);

			ShowAsDropDown(new Rect(popupPosition, size), size);
		}

		private void OnEnable()
		{
			m_closeButtonIcon = EditorGUIUtility.IconContent("d_winbtn_win_close");
			m_closeButtonOptions = new[] { GUILayout.Width(32f) };
		}

		private void OnGUI()
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField(m_addPopupValue.valueType.Name, EditorStyles.toolbarButton);

			if (GUILayout.Button(m_closeButtonIcon, EditorStyles.toolbarButton, m_closeButtonOptions))
			{
				Close();
				return;
			}

			EditorGUILayout.EndHorizontal();

			m_key = EditorGUILayout.TextField("Key", m_key);

			m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);

			m_addPopupValue.DrawValue("Value");

			EditorGUILayout.EndScrollView();

			if (GUILayout.Button("OK"))
			{
				m_addPopupValue.Set(m_key, m_blackboard);
				Close();
			}
		}
	}
}
