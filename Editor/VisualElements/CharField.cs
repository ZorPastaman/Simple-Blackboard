﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="char"/>.
	/// </summary>
	public sealed class CharField : BaseField<char>
	{
		[NotNull] private static readonly string s_defaultCharString = default(char).ToString();

		[NotNull] private static readonly EventCallback<ChangeEvent<string>, CharField> s_onInputChanged = (c, field) =>
		{
			if (string.IsNullOrEmpty(c.newValue))
			{
				field.m_input.value = s_defaultCharString;
			}

			field.value = field.m_input.value[0];
		};

		[NotNull] private readonly TextField m_input;

		public CharField() : this(null)
		{
		}

		public CharField([CanBeNull] string label)
			: this(label, new TextField(1, false, false, '*'))
		{
		}

		private CharField([CanBeNull] string label, [NotNull] TextField visualInput) : base(label, visualInput)
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
