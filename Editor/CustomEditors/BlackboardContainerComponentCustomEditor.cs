// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor;
using Zor.SimpleBlackboard.EditorTools;

namespace Zor.SimpleBlackboard.Components
{
	[CustomEditor(typeof(SimpleBlackboardContainer))]
	public sealed class BlackboardContainerComponentCustomEditor : Editor
	{
		private bool m_constantRepaint;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!EditorApplication.isPlaying)
			{
				return;
			}

			EditorGUILayout.Separator();
			m_constantRepaint = EditorGUILayout.Toggle("Require Constant Repaint", m_constantRepaint);

			var blackboardContainer = (SimpleBlackboardContainer)target;
			BlackboardEditor.DrawBlackboard(blackboardContainer.blackboard);
		}

		public override bool RequiresConstantRepaint()
		{
			return m_constantRepaint;
		}
	}
}
