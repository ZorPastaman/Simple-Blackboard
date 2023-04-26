// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a dropdown field for entering a <see cref="LayerMask"/>.
	/// </summary>
	public sealed class LayerMaskBaseField : BaseField<LayerMask>
	{
		[NotNull] private static readonly EventCallback<ChangeEvent<int>, LayerMaskBaseField> s_onInputChanged = (c, field) =>
		{
			field.value = c.newValue;
		};

		[NotNull] private readonly LayerMaskField m_input;

		public LayerMaskBaseField() : this(null)
		{
		}

		public LayerMaskBaseField(int defaultMask) : this(null, defaultMask)
		{
		}

		public LayerMaskBaseField([CanBeNull] string label, int defaultMask = 0)
			: this(label, new LayerMaskField(defaultMask))
		{
		}

		private LayerMaskBaseField([CanBeNull] string label, [NotNull] LayerMaskField visualInput)
			: base(label, visualInput)
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
