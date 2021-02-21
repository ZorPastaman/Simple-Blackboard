// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.BlackboardValueViews;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorWindows
{
	/// <summary>
	/// Popup used for adding new properties to <see cref="Zor.SimpleBlackboard.Core.Blackboard"/> in the editor.
	/// </summary>
	internal sealed class AddPopup : EditorWindow
	{
		private TextField m_keyTextField;
		private VisualElement m_valueField;

		/// <summary>
		/// Sets necessary parameters to <see cref="AddPopup"/>.
		/// </summary>
		/// <param name="blackboard">New property is added to this.</param>
		/// <param name="key">Initial key. It can be changed in GUI.</param>
		/// <param name="valueView">Value view wrapper.</param>
		/// <param name="popupPosition">Position of the popup in the editor space.</param>
		public void Setup([NotNull] Blackboard blackboard, [NotNull] string key,
			[NotNull] IBlackboardValueView valueView, Vector2 popupPosition)
		{
			var size = new Vector2(450f, EditorGUIUtility.singleLineHeight * 8f
				+ EditorGUIUtility.standardVerticalSpacing * 6f);
			ShowAsDropDown(new Rect(popupPosition, size), size);

			VisualElement root = rootVisualElement;

			var toolbar = new Toolbar();
			toolbar.style.justifyContent = Justify.SpaceBetween;
			root.Add(toolbar);

			var titleLabel = new Label(valueView.valueType.Name);
			IStyle titleLabelStyle = titleLabel.style;
			titleLabelStyle.unityFontStyleAndWeight = FontStyle.Bold;
			titleLabelStyle.unityTextAlign = TextAnchor.MiddleLeft;
			toolbar.Add(titleLabel);

			var closeButton = new ToolbarButton(Close);
			IStyle closeButtonStyle = closeButton.style;
			closeButtonStyle.width = 32f;
			closeButtonStyle.alignItems = Align.Center;
			toolbar.Add(closeButton);

			GUIContent closeButtonIcon = EditorGUIUtility.IconContent("d_winbtn_win_close");
			if (closeButtonIcon != null && closeButtonIcon.image != null)
			{
				Texture image = closeButtonIcon.image;
				var closeButtonImage = new Image {image = image};
				IStyle closeButtonImageStyle = closeButtonImage.style;
				closeButtonImageStyle.height = image.height;
				closeButtonImageStyle.width = image.width;
				closeButton.Add(closeButtonImage);
			}
			else
			{
				closeButton.text = "x";
				closeButtonStyle.unityTextAlign = TextAnchor.MiddleCenter;
				closeButtonStyle.fontSize = 18;
			}

			var scrollView = new ScrollView();
			root.Add(scrollView);

			m_keyTextField = new TextField("Key") {value = key};
			scrollView.Add(m_keyTextField);

			m_valueField = valueView.CreateVisualElement("Value");
			scrollView.Add(m_valueField);

			var okButton = new Button(() =>
			{
				valueView.SetValue(m_keyTextField.value, m_valueField, blackboard);
				Close();
			}) {text = "OK"};
			root.Add(okButton);
		}
	}
}
