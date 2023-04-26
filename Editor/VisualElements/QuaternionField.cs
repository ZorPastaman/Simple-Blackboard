﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.VisualElements
{
	/// <summary>
	/// Makes a text field for entering a <see cref="Quaternion"/>.
	/// </summary>
	public sealed class QuaternionField : BaseField<Quaternion>
	{
		[NotNull]
		private static readonly EventCallback<ChangeEvent<Vector4>, QuaternionField> s_onInputChanged = (c, field) =>
		{
			field.value = ToQuaternion(c.newValue);
		};

		[NotNull] private readonly Vector4Field m_input;

		public QuaternionField() : this(null)
		{
		}

		public QuaternionField([CanBeNull] string label) : this(label, new Vector4Field())
		{
		}

		private QuaternionField([CanBeNull] string label, [NotNull] Vector4Field visualInput) : base(label, visualInput)
		{
			visualInput.RegisterCallback(s_onInputChanged, this);
			m_input = visualInput;
		}

		public override void SetValueWithoutNotify(Quaternion newValue)
		{
			base.SetValueWithoutNotify(newValue);
			m_input.SetValueWithoutNotify(ToVector4(newValue));
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
