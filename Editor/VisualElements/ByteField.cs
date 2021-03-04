// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="byte"/>.
	/// </summary>
	public sealed class ByteField : BaseField<byte>
	{
		private static readonly EventCallback<ChangeEvent<int>, ByteField> s_onInputChanged = (c, field) =>
		{
			field.m_subField.value = field.value = (byte)Mathf.Clamp(c.newValue, byte.MinValue, byte.MaxValue);
		};

		private readonly IntegerField m_subField;

		public ByteField() : this(-1)
		{
		}

		public ByteField(int maxLength) : this(null, maxLength)
		{
		}

		public ByteField(string label, int maxLength = -1)
			: this(new IntegerField(label, maxLength), new VisualElement())
		{
		}

		private ByteField(IntegerField subField, VisualElement visualInput) : base(null, visualInput)
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

		public override void SetValueWithoutNotify(byte newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify(newValue);
		}
	}
}
