// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class LayerMaskBlackboardValueView : BlackboardValueView<LayerMask>
	{
		public override LayerMask DrawValue(string label, LayerMask value)
		{
			return EditorGUILayout.MaskField(label, value, InternalEditorUtility.layers);
		}
	}
}
