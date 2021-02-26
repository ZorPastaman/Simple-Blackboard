// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	public sealed class QuaternionField : BaseField<Quaternion>
	{
		private static readonly EventCallback<ChangeEvent<Vector4>, QuaternionField> s_onInputChanged = (c, field) =>
		{
			field.value = ToQuaternion(c.newValue);
		};

		private readonly Vector4Field m_subField;

		public QuaternionField() : this(null)
		{
		}

		public QuaternionField(string label) : this(new Vector4Field(label), new VisualElement())
		{
		}

		private QuaternionField(Vector4Field subField, VisualElement visualInput) : base(null, visualInput)
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

		public override void SetValueWithoutNotify(Quaternion newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_subField.SetValueWithoutNotify(ToVector4(newValue));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		private static Vector4 ToVector4(Quaternion quaternion)
		{
			return new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		private static Quaternion ToQuaternion(Vector4 vector)
		{
			return new Quaternion(vector.x, vector.y, vector.z, vector.w);
		}
	}
}
