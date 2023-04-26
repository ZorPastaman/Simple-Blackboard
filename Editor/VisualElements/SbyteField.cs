// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="sbyte"/>.
	/// </summary>
	public sealed class SbyteField : BaseField<sbyte>
	{
		[NotNull] private static readonly EventCallback<ChangeEvent<int>, SbyteField> s_onInputChanged = (c, field) =>
		{
			field.m_subField.value = field.value = (sbyte)Mathf.Clamp(c.newValue, sbyte.MinValue, sbyte.MaxValue);
		};

		[NotNull] private readonly IntegerField m_subField;

		public SbyteField() : this(-1)
		{
		}

		public SbyteField(int maxLength) : this(null, maxLength)
		{
		}

		public SbyteField([CanBeNull] string label, int maxLength = -1)
			: this(new IntegerField(label, maxLength), new VisualElement())
		{
		}

		private SbyteField([NotNull] IntegerField subField, [NotNull] VisualElement visualInput)
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

		public override void SetValueWithoutNotify(sbyte newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify(newValue);
		}
	}
}
