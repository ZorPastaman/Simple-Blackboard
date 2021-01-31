// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Inherit this if you need to draw <see cref="Object"/> with EditorGUILayout.ObjectField.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	public abstract class UnityObjectBlackboardValueView<T> : BlackboardValueView<T> where T : Object
	{
		public sealed override T DrawValue(string label, T value)
		{
			return EditorGUILayout.ObjectField(label, value, typeof(T), true) as T;
		}
	}
}
