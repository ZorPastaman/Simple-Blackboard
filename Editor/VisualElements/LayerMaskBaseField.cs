// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	public sealed class LayerMaskBaseField : BaseField<LayerMask>
	{
		private static readonly EventCallback<ChangeEvent<int>, LayerMaskBaseField> s_onInputChanged = (c, field) =>
		{
			field.value = c.newValue;
		};

		private readonly LayerMaskField m_input;

		public LayerMaskBaseField() : this(null)
		{
		}

		public LayerMaskBaseField(int defaultMask) : this(null, defaultMask)
		{
		}

		public LayerMaskBaseField(string label, int defaultMask = 0) : this(label, new LayerMaskField(defaultMask))
		{
		}

		private LayerMaskBaseField(string label, LayerMaskField visualInput) : base(label, visualInput)
		{
			visualInput.RegisterCallback(s_onInputChanged, this);
			m_input = visualInput;
		}

		public override void SetValueWithoutNotify(LayerMask newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_input.SetValueWithoutNotify(newValue);
		}
	}
}
