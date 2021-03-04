// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorWindows
{
	/// <summary>
	/// Popup used for adding new properties to <see cref="Blackboard"/> in the editor.
	/// </summary>
	public sealed class AddPopup : EditorWindow
	{
		/// <summary>
		/// Sets necessary parameters to <see cref="AddPopup"/> and shows it as a popup.
		/// </summary>
		/// <param name="blackboard">New property is added to this.</param>
		/// <param name="key">Initial key. It can be changed in GUI.</param>
		/// <param name="baseField">Value view.</param>
		/// <param name="popupPosition">Position of the popup in the editor space.</param>
		/// <typeparam name="T">Value type.</typeparam>
		/// <remarks>
		/// Must be called just after creation of <see cref="AddPopup"/>.
		/// </remarks>
		[UsedImplicitly]
		public void Setup<T>([NotNull] Blackboard blackboard, [NotNull] string key,
			[NotNull] BaseField<T> baseField, Vector2 popupPosition)
		{
			var size = new Vector2(450f, EditorGUIUtility.singleLineHeight * 8f
				+ EditorGUIUtility.standardVerticalSpacing * 6f);
			ShowAsDropDown(new Rect(popupPosition, size), size);

			VisualElement root = rootVisualElement;

			var toolbar = new Toolbar();
			toolbar.style.justifyContent = Justify.SpaceBetween;
			root.Add(toolbar);

			var titleLabel = new Label(typeof(T).Name);
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

			var keyTextField = new TextField("Key") {value = key};
			scrollView.Add(keyTextField);

			baseField.label = "Value";
			scrollView.Add(baseField);

			var okButton = new Button(() =>
			{
				blackboard.SetObjectValue(typeof(T), new BlackboardPropertyName(keyTextField.value), baseField.value);
				Close();
			}) {text = "OK"};
			root.Add(okButton);
		}
	}
}
