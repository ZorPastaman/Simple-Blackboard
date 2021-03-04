// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="short"/>.
	/// </summary>
	public sealed class ShortField : BaseField<short>
	{
		[NotNull] private static readonly EventCallback<ChangeEvent<int>, ShortField> s_onInputChanged = (c, field) =>
		{
			field.m_subField.value = field.value = (short)Mathf.Clamp(c.newValue, short.MinValue, short.MaxValue);
		};

		[NotNull] private readonly IntegerField m_subField;

		public ShortField() : this(-1)
		{
		}

		public ShortField(int maxLength) : this(null, maxLength)
		{
		}

		public ShortField([CanBeNull] string label, int maxLength = -1)
			: this(new IntegerField(label, maxLength), new VisualElement())
		{
		}

		private ShortField([NotNull] IntegerField subField, [NotNull] VisualElement visualInput)
			: base(null, visualInput)
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

		public override void SetValueWithoutNotify(short newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify(newValue);
		}
	}
}
