// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	public sealed class LayerMaskWrapper : BaseField<LayerMask>
	{
		private static readonly EventCallback<ChangeEvent<int>, LayerMaskWrapper> s_onInputChanged = (c, field) =>
		{
			field.value = field.m_subField.value;
		};

		private readonly LayerMaskField m_subField;

		public LayerMaskWrapper() : this(null)
		{
		}

		public LayerMaskWrapper(int defaultMask) : this(null, defaultMask)
		{
		}

		public LayerMaskWrapper(string label) : this(label, 0)
		{
		}

		public LayerMaskWrapper(string label, int defaultMask)
			: this(new LayerMaskField(label, defaultMask), new VisualElement())
		{
		}

		private LayerMaskWrapper(LayerMaskField subField, VisualElement visualInput) : base(null, visualInput)
		{
			m_subField = subField;
			IStyle subFieldStyle = subField.style;
			subFieldStyle.flexGrow = 1f;
			subFieldStyle.marginLeft = subFieldStyle.marginRight =
				subFieldStyle.marginTop = subFieldStyle.marginBottom = 0f;
			subField.RegisterCallback(s_onInputChanged, this);
			Add(subField);

			visualInput.style.flexGrow = 0f;
		}

		public override void SetValueWithoutNotify(LayerMask newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify(newValue);
		}
	}
}
