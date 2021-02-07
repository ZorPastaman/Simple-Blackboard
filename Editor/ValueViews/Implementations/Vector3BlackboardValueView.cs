// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector3BlackboardValueView : BlackboardValueView<Vector3>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new Vector3Field(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var vector3Field = (Vector3Field)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), vector3Field.value);
		}

		public override Vector3 DrawValue(string label, Vector3 value)
		{
			return EditorGUILayout.Vector3Field(label, value);
		}
	}
}
