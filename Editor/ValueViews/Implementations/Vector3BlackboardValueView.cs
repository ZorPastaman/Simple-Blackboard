// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Creates a view for a property of type <see cref="Vector3"/>.
	/// </summary>
	[UsedImplicitly]
	public sealed class Vector3BlackboardValueView : BlackboardValueView<Vector3>
	{
		/// <inheritdoc/>
		[Pure]
		public override BaseField<Vector3> CreateBaseField(string label)
		{
			return new Vector3Field(label);
		}

		/// <inheritdoc/>
		[Pure]
		public override Vector3 DrawValue(string label, Vector3 value)
		{
			return EditorGUILayout.Vector3Field(label, value);
		}
	}
}
