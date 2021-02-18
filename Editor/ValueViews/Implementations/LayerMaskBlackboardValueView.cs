// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class LayerMaskBlackboardValueView : BlackboardValueView<LayerMask>
	{
		public override VisualElement CreateVisualElement(string label, VisualElement blackboardRoot = null)
		{
			var layerMaskField = new LayerMaskField(label);

			if (blackboardRoot != null)
			{
				layerMaskField.RegisterValueChangedCallback(c =>
				{
					if (blackboardRoot.userData is Blackboard blackboard)
					{
						blackboard.SetStructValue(new BlackboardPropertyName(label), (LayerMask)layerMaskField.value);
					}
				});
			}

			return layerMaskField;
		}

		public override void UpdateValue(VisualElement visualElement, LayerMask value)
		{
			var layerMaskField = (LayerMaskField)visualElement;
			layerMaskField.value = value;
		}

		public override void SetValue(string key, VisualElement visualElement, Blackboard blackboard)
		{
			var layerMaskField = (LayerMaskField)visualElement;
			blackboard.SetStructValue(new BlackboardPropertyName(key), (LayerMask)layerMaskField.value);
		}

		public override LayerMask DrawValue(string label, LayerMask value)
		{
			return EditorGUILayout.MaskField(label, value, InternalEditorUtility.layers);
		}
	}
}
