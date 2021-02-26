// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	public sealed class PropertyNameField : BaseField<PropertyName>
	{
		private static readonly EventCallback<ChangeEvent<string>, PropertyNameField> s_onInputChanged = (c, field) =>
		{
			field.value = c.newValue;
		};

		private readonly TextField m_subField;

		public PropertyNameField() : this(-1)
		{
		}

		public PropertyNameField(int maxLength) : this(null, maxLength)
		{
		}

		public PropertyNameField(string label, int maxLength = -1)
			: this(new TextField(label, maxLength, false, false, '*'), new VisualElement())
		{
		}

		private PropertyNameField(TextField subField, VisualElement visualInput) : base(null, visualInput)
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

		public override void SetValueWithoutNotify(PropertyName newValue)
		{
			string val = newValue.ToString();
			val = val.Substring(0, val.IndexOf(':'));
			base.SetValueWithoutNotify(val);
			m_subField.SetValueWithoutNotify(val);
		}
	}
}
