// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	public sealed class UnityObjectBaseField<T> : BaseField<T> where T : Object
	{
		private static readonly EventCallback<ChangeEvent<Object>, UnityObjectBaseField<T>> s_onInputChanged = (c, field) =>
		{
			field.value = c.newValue as T;
		};

		private readonly ObjectField m_input;

		public UnityObjectBaseField() : this(null)
		{
		}

		public UnityObjectBaseField(string label, bool allowSceneObjects = true)
			: this(label, allowSceneObjects, new ObjectField())
		{
		}

		private UnityObjectBaseField(string label, bool allowSceneObjects, ObjectField visualInput)
			: base(label, visualInput)
		{
			visualInput.objectType = typeof(T);
			visualInput.allowSceneObjects = allowSceneObjects;
			visualInput.RegisterCallback(s_onInputChanged, this);
			m_input = visualInput;
		}

		public override void SetValueWithoutNotify(T newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_input.SetValueWithoutNotify(newValue);
		}
	}
}
