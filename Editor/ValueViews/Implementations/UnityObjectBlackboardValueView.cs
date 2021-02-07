// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Inherit this if you need to draw <see cref="Object"/> with EditorGUILayout.ObjectField.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	public abstract class UnityObjectBlackboardValueView<T> : BlackboardValueView<T> where T : Object
	{
		public sealed override VisualElement CreateVisualElement(string label)
		{
			return new ObjectField(label) {objectType = typeof(T), allowSceneObjects = true};
		}

		public sealed override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var objectField = (ObjectField)visualElement;
			blackboard.SetObjectValue(typeof(T), new BlackboardPropertyName(key), objectField.value as T);
		}

		public sealed override T DrawValue(string label, T value)
		{
			return EditorGUILayout.ObjectField(label, value, typeof(T), true) as T;
		}
	}
}
