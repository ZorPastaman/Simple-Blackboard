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
				field.m_input.value = s_defaultCharString;
			}

			field.value = field.m_input.value[0];
		};

		private readonly TextField m_input;

		public CharField() : this(null)
		{
		}

		public CharField(string label) : this(label, new TextField(1, false, false, '*'))
		{
		}

		private CharField(string label, TextField visualInput) : base(label, visualInput)
		{
			visualInput.RegisterCallback(s_onInputChanged, this);
			m_input = visualInput;
		}

		public override void SetValueWithoutNotify(char newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_input.SetValueWithoutNotify(newValue.ToString());
		}
	}
}
