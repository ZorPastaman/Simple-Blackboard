// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.EditorTools;
using Zor.SimpleBlackboard.Helpers;

namespace Zor.SimpleBlackboard.Components
{
	/// <summary>
	/// <para>Custom editor for <see cref="SimpleBlackboardContainer"/>.</para>
	/// <para>It shows fields of the component and a contained <see cref="Blackboard"/> when available.</para>
	/// </summary>
	/// <remarks>
	/// The editor constantly updates the view of a contained <see cref="Blackboard"/> if a mouse is over the component
	/// view or if the toggle Require Constant Repaint is toggled on.
	/// </remarks>
	[CustomEditor(typeof(SimpleBlackboardContainer))]
	public sealed class SimpleBlackboardContainerCustomEditor : Editor
	{
		private static readonly EventCallback<MouseEnterEvent, SimpleBlackboardContainerCustomEditor> s_onMouseEnter =
			(c, editor) =>
			{
				editor.m_mouseEntered = true;
				editor.ResolveUpdate();
			};
		private static readonly EventCallback<MouseLeaveEvent, SimpleBlackboardContainerCustomEditor> s_onMouseLeave =
			(c, editor) =>
			{
				editor.m_mouseEntered = false;
				editor.ResolveUpdate();
			};
		private static readonly EventCallback<ChangeEvent<bool>, SimpleBlackboardContainerCustomEditor> s_onToggled =
			(c, editor) =>
			{
				editor.ResolveUpdate();
			};

		private Toggle m_requiresConstantRepaintToggle;
		private Foldout m_blackboardFoldout;
		private VisualElement m_blackboardVisualElement;

		private Action<PlayModeStateChange> m_onPlayerModeStateChanged;
		private EditorApplication.CallbackFunction m_onUpdate;

		private bool m_mouseEntered;
		private bool m_updating;

		[NotNull]
		public override VisualElement CreateInspectorGUI()
		{
			VisualElement root = UIElementsHelper.CreateDefaultObjectGUI(serializedObject);
			root.RegisterCallback(s_onMouseEnter, this);
			root.RegisterCallback(s_onMouseLeave, this);

			m_requiresConstantRepaintToggle = new Toggle("Require Constant Repaint");
			m_requiresConstantRepaintToggle.RegisterCallback(s_onToggled, this);
			root.Add(m_requiresConstantRepaintToggle);

			m_blackboardFoldout = new Foldout {text = "Blackboard"};
			root.Add(m_blackboardFoldout);

			m_blackboardVisualElement = BlackboardEditor.CreateBlackboardVisualElement();
			m_blackboardFoldout.Add(m_blackboardVisualElement);

			// Unsubscription and subscription to avoid double subscription.
			EditorApplication.playModeStateChanged -= m_onPlayerModeStateChanged;
			EditorApplication.playModeStateChanged += m_onPlayerModeStateChanged;
			OnPlaymodeChanged(EditorApplication.isPlaying);

			ResolveUpdate();
			UpdateBlackboardView();

			return root;
		}

		public override bool RequiresConstantRepaint()
		{
			return m_requiresConstantRepaintToggle != null && m_requiresConstantRepaintToggle.value;
		}

		private void Awake()
		{
			m_onPlayerModeStateChanged = OnPlaymodeStateChanged;
			m_onUpdate = UpdateBlackboardView;
		}

		private void OnDestroy()
		{
			EditorApplication.playModeStateChanged -= m_onPlayerModeStateChanged;
			EditorApplication.update -= m_onUpdate;
		}

		private void ResolveUpdate()
		{
			bool shouldUpdate = m_requiresConstantRepaintToggle.value | m_mouseEntered;

			if (shouldUpdate == m_updating)
			{
				return;
			}

			if (shouldUpdate)
			{
				EditorApplication.update += m_onUpdate;
			}
			else
			{
				EditorApplication.update -= m_onUpdate;
			}

			m_updating = shouldUpdate;
		}

		private void UpdateBlackboardView()
		{
			var blackboardContainer = (SimpleBlackboardContainer)target;
			Blackboard blackboard = blackboardContainer.blackboard;
			BlackboardEditor.UpdateBlackboardVisualElement(m_blackboardVisualElement, blackboard);
		}

		private void OnPlaymodeStateChanged(PlayModeStateChange playModeStateChange)
		{
			OnPlaymodeChanged(playModeStateChange == PlayModeStateChange.EnteredPlayMode);
		}

		private void OnPlaymodeChanged(bool isPlaying)
		{
			m_requiresConstantRepaintToggle.value &= isPlaying;

			StyleEnum<DisplayStyle> displayStyle = isPlaying
				? DisplayStyle.Flex
				: DisplayStyle.None;

			m_requiresConstantRepaintToggle.style.display = displayStyle;
			m_blackboardFoldout.style.display = displayStyle;
		}
	}
}
