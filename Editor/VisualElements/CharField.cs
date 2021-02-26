// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	public sealed class CharField : BaseField<char>
	{
		private static readonly string s_defaultCharString = default(char).ToString();

		private static readonly EventCallback<ChangeEvent<string>, CharField> s_onInputChanged = (c, field) =>
		{
			if (string.IsNullOrEmpty(c.newValue))
			{
				field.m_subField.value = s_defaultCharString;
			}

			field.value = field.m_subField.value[0];
		};

		private readonly TextField m_subField;

		public CharField() : this(null)
		{
		}

		public CharField(string label)
			: this(new TextField(label, 1, false, false, '*'), new VisualElement())
		{
		}

		private CharField(TextField subField, VisualElement visualInput) : base(null, visualInput)
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

		public override void SetValueWithoutNotify(char newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify(newValue.ToString());
		}
	}
}
