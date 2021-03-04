// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a select field for entering a <see cref="Object"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class UnityObjectBaseField<T> : BaseField<T> where T : Object
	{
		[NotNull] private static readonly EventCallback<ChangeEvent<Object>, UnityObjectBaseField<T>> s_onInputChanged =
			(c, field) =>
			{
				field.value = c.newValue as T;
			};

		[NotNull] private readonly ObjectField m_input;

		public UnityObjectBaseField() : this(null)
		{
		}

		public UnityObjectBaseField([CanBeNull] string label, bool allowSceneObjects = true)
			: this(label, allowSceneObjects, new ObjectField())
		{
		}

		private UnityObjectBaseField([CanBeNull] string label, bool allowSceneObjects,
			[NotNull] ObjectField visualInput) : base(label, visualInput)
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
