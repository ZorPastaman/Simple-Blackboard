// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Inherit this if you need to draw <see cref="Object"/> with <see cref="EditorGUILayout.ObjectField"/>
	/// or <see cref="UnityObjectBaseField{T}"/>.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	public abstract class UnityObjectBlackboardValueView<T> : BlackboardValueView<T> where T : Object
	{
		/// <inheritdoc/>
		[Pure]
		public sealed override BaseField<T> CreateBaseField(string label)
		{
			return new UnityObjectBaseField<T>(label);
		}

		/// <inheritdoc/>
		[Pure]
		public sealed override T DrawValue(string label, T value)
		{
			return EditorGUILayout.ObjectField(label, value, typeof(T), true) as T;
		}
	}
}
