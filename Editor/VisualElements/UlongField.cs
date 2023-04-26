// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="ulong"/>
	/// </summary>
	public sealed class UlongField : BaseField<ulong>
	{
		[NotNull] private static readonly EventCallback<ChangeEvent<long>, UlongField> s_onInputChanged = (c, field) =>
		{
			if (c.newValue < (long)ulong.MinValue)
			{
				field.m_subField.value = (long)ulong.MinValue;
			}

			field.value = (ulong)field.m_subField.value;
		};

		[NotNull] private readonly LongField m_subField;

		public UlongField() : this(-1)
		{
		}

		public UlongField(int maxLength) : this(null, maxLength)
		{
		}

		public UlongField([CanBeNull] string label, int maxLength = -1)
			: this(new LongField(label, maxLength), new VisualElement())
		{
		}

		private UlongField([NotNull] LongField subField, [NotNull] VisualElement visualInput)
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

		public override void SetValueWithoutNotify(ulong newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify((long)newValue);
		}
	}
}
