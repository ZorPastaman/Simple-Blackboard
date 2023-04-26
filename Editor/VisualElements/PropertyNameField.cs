// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="PropertyName"/>.
	/// </summary>
	public sealed class PropertyNameField : BaseField<PropertyName>
	{
		[NotNull]
		private static readonly EventCallback<ChangeEvent<string>, PropertyNameField> s_onInputChanged = (c, field) =>
		{
			field.value = c.newValue;
		};

		[NotNull] private readonly TextField m_input;

		public PropertyNameField() : this(-1)
		{
		}

		public PropertyNameField(int maxLength) : this(null, maxLength)
		{
		}

		public PropertyNameField([CanBeNull] string label, int maxLength = -1)
			: this(label, new TextField(maxLength, false, false, '*'))
		{
		}

		private PropertyNameField([CanBeNull] string label, [NotNull] TextField visualInput) : base(label, visualInput)
		{
			visualInput.RegisterCallback(s_onInputChanged, this);
			m_input = visualInput;
		}

		public override void SetValueWithoutNotify(PropertyName newValue)
		{
			string val = newValue.ToString();
			val = val.Substring(0, val.IndexOf(':'));
			base.SetValueWithoutNotify(val);
			m_input.SetValueWithoutNotify(val);
		}
	}
}
