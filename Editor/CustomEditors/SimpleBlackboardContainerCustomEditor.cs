// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.EditorTools;
using Zor.SimpleBlackboard.Helpers;

namespace Zor.SimpleBlackboard.Components
{
	[CustomEditor(typeof(SimpleBlackboardContainer))]
	public sealed class SimpleBlackboardContainerCustomEditor : Editor
	{
		private Toggle m_requiresConstantRepaintToggle;
		private Foldout m_blackboardFoldout;
		private VisualElement m_blackboardVisualElement;

		private Action<PlayModeStateChange> m_onPlayerModeStateChanged;
		private EditorApplication.CallbackFunction m_onUpdate;

		public override VisualElement CreateInspectorGUI()
		{
			VisualElement root = UIElementsHelper.CreateDefaultObjectGUI(serializedObject);

			m_requiresConstantRepaintToggle = new Toggle("Require Constant Repaint");
			root.Add(m_requiresConstantRepaintToggle);

			m_blackboardFoldout = new Foldout {text = "Blackboard"};
			root.Add(m_blackboardFoldout);

			m_blackboardVisualElement = BlackboardEditor.CreateBlackboardVisualElement();
			m_blackboardFoldout.Add(m_blackboardVisualElement);

			EditorApplication.update -= m_onUpdate;
			EditorApplication.update += m_onUpdate;

			EditorApplication.playModeStateChanged -= m_onPlayerModeStateChanged;
			EditorApplication.playModeStateChanged += m_onPlayerModeStateChanged;
			OnPlaymodeChanged(EditorApplication.isPlaying);

			return root;
		}

		public override bool RequiresConstantRepaint()
		{
			return m_requiresConstantRepaintToggle != null && m_requiresConstantRepaintToggle.value;
		}

		private void Awake()
		{
			m_onPlayerModeStateChanged = OnPlaymodeStateChanged;
			m_onUpdate = OnUpdate;
		}

		private void OnDestroy()
		{
			EditorApplication.playModeStateChanged -= m_onPlayerModeStateChanged;
			EditorApplication.update -= m_onUpdate;
		}

		private void OnUpdate()
		{
			BlackboardEditor.UpdateBlackboardVisualElement(m_blackboardVisualElement,
				((SimpleBlackboardContainer)target).blackboard);
		}

		private void OnPlaymodeStateChanged(PlayModeStateChange playModeStateChange)
		{
			OnPlaymodeChanged(playModeStateChange == PlayModeStateChange.EnteredPlayMode);
		}

		private void OnPlaymodeChanged(bool isPlaying)
		{
			StyleEnum<DisplayStyle> displayStyle = isPlaying
				? DisplayStyle.Flex
				: DisplayStyle.None;

			m_requiresConstantRepaintToggle.style.display = displayStyle;
			m_blackboardFoldout.style.display = displayStyle;
		}
	}
}
