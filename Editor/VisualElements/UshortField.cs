// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="ushort"/>.
	/// </summary>
	public sealed class UshortField : BaseField<ushort>
	{
		private static readonly EventCallback<ChangeEvent<int>, UshortField> s_onInputChanged = (c, field) =>
		{
			field.m_subField.value = field.value = (ushort)Mathf.Clamp(c.newValue, ushort.MinValue, ushort.MaxValue);
		};

		private readonly IntegerField m_subField;

		public UshortField() : this(-1)
		{
		}

		public UshortField(int maxLength) : this(null, maxLength)
		{
		}

		public UshortField(string label, int maxLength = -1)
			: this(new IntegerField(label, maxLength), new VisualElement())
		{
		}

		private UshortField(IntegerField subField, VisualElement visualInput) : base(null, visualInput)
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

		public override void SetValueWithoutNotify(ushort newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify(newValue);
		}
	}
}
