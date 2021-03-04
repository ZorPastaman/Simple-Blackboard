// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="uint"/>.
	/// </summary>
	public sealed class UintField : BaseField<uint>
	{
		[NotNull] private static readonly EventCallback<ChangeEvent<long>, UintField> s_onInputChanged = (c, field) =>
		{
			field.m_subField.value = field.value = (uint)Math.Max(Math.Min(c.newValue, uint.MaxValue), uint.MinValue);
		};

		[NotNull] private readonly LongField m_subField;

		public UintField() : this(-1)
		{
		}

		public UintField(int maxLength) : this(null, maxLength)
		{
		}

		public UintField([CanBeNull] string label, int maxLength = -1)
			: this(new LongField(label, maxLength), new VisualElement())
		{
		}

		private UintField([NotNull] LongField subField, [NotNull] VisualElement visualInput)
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

		public override void SetValueWithoutNotify(uint newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify(newValue);
		}
	}
}
