// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;
using Zor.SimpleBlackboard.VisualElements;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class LayerMaskBlackboardValueView : BlackboardValueView<LayerMask, LayerMask, LayerMaskWrapper>
	{
		public override LayerMaskWrapper CreateBaseField(string label, VisualElement blackboardRoot = null)
		{
			var layerMaskWrapper = new LayerMaskWrapper(label);

			if (blackboardRoot != null)
			{
				layerMaskWrapper.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), layerMaskWrapper.value);
					}
				});
			}

			return layerMaskWrapper;
		}

		public override void UpdateValue(VisualElement visualElement, LayerMask value)
		{
			var layerMaskWrapper = (LayerMaskWrapper)visualElement;
			layerMaskWrapper.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var layerMaskWrapper = (LayerMaskWrapper)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), layerMaskWrapper.value);
		}

		public override LayerMask DrawValue(string label, LayerMask value)
		{
			return EditorGUILayout.MaskField(label, value, InternalEditorUtility.layers);
		}
	}
}
