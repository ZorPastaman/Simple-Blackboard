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
	public sealed class Vector3IntBlackboardValueView : BlackboardValueView<Vector3Int>
	{
		public override VisualElement CreateVisualElement(string label)
		{
			return new Vector3IntField(label);
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var vector3IntField = (Vector3IntField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), vector3IntField.value);
		}

		public override Vector3Int DrawValue(string label, Vector3Int value)
		{
			return EditorGUILayout.Vector3IntField(label, value);
		}
	}
}
