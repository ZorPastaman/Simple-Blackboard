// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.EditorTools;
using Zor.SimpleBlackboard.Helpers;

namespace Zor.SimpleBlackboard.Components
{
	[CustomEditor(typeof(SimpleBlackboardContainer))]
	public sealed class SimpleBlackboardContainerCustomEditor : Editor
	{
		private const string ConstantRepaintPropertyName = "constantRepaint";

		private Toggle m_requiresConstantRepaint;

		public override VisualElement CreateInspectorGUI()
		{
			VisualElement root = UIElementsHelper.CreateDefaultObjectGUI(serializedObject);

			m_requiresConstantRepaint = new Toggle("Require Constant Repaint");
			root.Add(m_requiresConstantRepaint);

			var imguiContainer = new IMGUIContainer(DrawBlackboard);
			root.Add(imguiContainer);

			return root;
		}

		public override bool RequiresConstantRepaint()
		{
			bool playing = EditorApplication.isPlaying;
			m_requiresConstantRepaint.style.display = playing ? DisplayStyle.Flex : DisplayStyle.None;

			return playing & m_requiresConstantRepaint.value;
		}

		private void DrawBlackboard()
		{
			if (!EditorApplication.isPlaying)
			{
				return;
			}

			var blackboardContainer = (SimpleBlackboardContainer)target;
			BlackboardEditor.DrawBlackboard(blackboardContainer.blackboard);
		}
	}
}
