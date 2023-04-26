// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.BlackboardValueViews;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <summary>
	/// Draws an editor for <see cref="BlackboardTable{T}"/>.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	internal abstract class BlackboardTableEditor<T> : BlackboardTableEditor_Base
	{
		[NotNull] private const string ContainerElementName = "Container";
		[NotNull] private const string BaseFieldElementName = "BaseField";

		[NotNull] private static readonly EqualityComparer<T> s_equalityComparer = EqualityComparer<T>.Default;

		[NotNull] private static readonly List<KeyValuePair<BlackboardPropertyName, T>> s_properties =
			new List<KeyValuePair<BlackboardPropertyName, T>>();

		[NotNull] private readonly BlackboardValueView<T> m_blackboardValueView;

		/// <summary>
		/// Creates a <see cref="BlackboardTableEditor{T}"/> using <paramref name="blackboardValueView"/> for drawing.
		/// </summary>
		/// <param name="blackboardValueView">
		/// This is used for drawing a property in <see cref="BlackboardTable{T}"/>
		/// </param>
		protected BlackboardTableEditor([NotNull] BlackboardValueView<T> blackboardValueView)
		{
			m_blackboardValueView = blackboardValueView;
		}

		/// <inheritdoc/>
		public override Type valueType
		{
			[Pure]
			get => typeof(T);
		}

		/// <inheritdoc/>
		public override void Draw(Blackboard blackboard)
		{
			try
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);

				EditorGUILayout.LabelField(valueType.Name, EditorStyles.boldLabel);

				GetProperties(blackboard, s_properties);
				s_properties.Sort((left, right)
					=> string.CompareOrdinal(left.Key.name, right.Key.name));

				for (int i = 0, count = s_properties.Count; i < count; ++i)
				{
					KeyValuePair<BlackboardPropertyName, T> property = s_properties[i];

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.BeginVertical();

					EditorGUI.BeginChangeCheck();

					BlackboardPropertyName key = property.Key;
					T newValue = m_blackboardValueView.DrawValue(key.name, property.Value);

					if (EditorGUI.EndChangeCheck())
					{
						SetValue(blackboard, key, newValue);
					}

					EditorGUILayout.EndVertical();

					if (GUILayout.Button(EditorGUIUtility.IconContent(RemoveButtonIconContentName),
						RemoveButtonOptions))
					{
						blackboard.RemoveObject(key);
					}

					EditorGUILayout.EndHorizontal();
				}

				EditorGUILayout.EndVertical();
			}
			finally
			{
				s_properties.Clear();
			}
		}

		/// <inheritdoc/>
		[Pure]
		public override VisualElement CreateTable()
		{
			var root = new Box();

			var label = new Label(valueType.Name);
			label.style.unityFontStyleAndWeight = FontStyle.Bold;
			root.Add(label);

			var container = new VisualElement {name = ContainerElementName};
			root.Add(container);

			return root;
		}

		/// <inheritdoc/>
		public override void UpdateTable(VisualElement tableRoot, VisualElement blackboardRoot)
		{
			if (!(blackboardRoot.userData is Blackboard blackboard))
			{
				return;
			}

			try
			{
				VisualElement container = tableRoot.Q(ContainerElementName);
				GetProperties(blackboard, s_properties);
				bool structureChanged = false;

				for (int i = container.childCount - 1; i >= 0; --i)
				{
					VisualElement propertyElement = container[i];

					if (!ContainsPropertyOfName(propertyElement.name))
					{
						container.RemoveAt(i);
						structureChanged = true;
					}
				}

				for (int i = 0, count = s_properties.Count; i < count; ++i)
				{
					KeyValuePair<BlackboardPropertyName, T> property = s_properties[i];
					BlackboardPropertyName key = property.Key;

					VisualElement propertyElement = container.Q(key.name);
					if (propertyElement == null)
					{
						propertyElement = CreatePropertyElement(blackboardRoot, key);
						container.Add(propertyElement);
						structureChanged = true;
					}

					var baseField = propertyElement.Q<BaseField<T>>(BaseFieldElementName);
					if (!s_equalityComparer.Equals(baseField.value, property.Value))
					{
						// We need to call Equals() ourselves despite that that check is called in BaseField.value
						// because TextValueField.value always allocates a new string.
						baseField.value = property.Value;
					}
				}

				if (structureChanged)
				{
					container.Sort((left, right) => string.CompareOrdinal(left.name, right.name));
				}
			}
			finally
			{
				s_properties.Clear();
			}
		}

		/// <summary>
		/// Gets properties of the type <typeparamref name="T"/> from <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">Blackboard source.</param>
		/// <param name="properties">Properties output.</param>
		protected abstract void GetProperties([NotNull] Blackboard blackboard,
			[NotNull] List<KeyValuePair<BlackboardPropertyName, T>> properties);

		/// <summary>
		/// Sets <paramref name="value"/> into <paramref name="blackboard"/>
		/// with <paramref name="key"/> as a property name.
		/// </summary>
		/// <param name="blackboard">Property is set into this.</param>
		/// <param name="key">Property name.</param>
		/// <param name="value">Property value.</param>
		protected abstract void SetValue([NotNull] Blackboard blackboard,
			BlackboardPropertyName key, [CanBeNull] T value);

		[Pure]
		private static bool ContainsPropertyOfName([NotNull] string name)
		{
			for (int i = 0, count = s_properties.Count; i < count; ++i)
			{
				if (s_properties[i].Key.name == name)
				{
					return true;
				}
			}

			return false;
		}

		[NotNull, Pure]
		private VisualElement CreatePropertyElement([NotNull] VisualElement blackboardRoot,
			BlackboardPropertyName propertyName)
		{
			string keyName = propertyName.name;

			var propertyElement = new VisualElement {name = keyName};
			propertyElement.style.flexDirection = FlexDirection.Row;

			BaseField<T> baseField = m_blackboardValueView.CreateBaseField(keyName);
			baseField.RegisterValueChangedCallback(c =>
			{
				if (blackboardRoot.userData is Blackboard blackboard)
				{
					SetValue(blackboard, new BlackboardPropertyName(keyName),
						c.newValue is T newValue ? newValue : default);
				}
			});
			baseField.name = BaseFieldElementName;
			baseField.style.flexGrow = 1f;
			propertyElement.Add(baseField);

			Button removeButton = CreateRemoveButton(blackboardRoot, propertyName);
			propertyElement.Add(removeButton);

			return propertyElement;
		}

		[NotNull, Pure]
		private static Button CreateRemoveButton([NotNull] VisualElement blackboardRoot,
			BlackboardPropertyName propertyName)
		{
			var removeButton = new Button(() =>
			{
				if (blackboardRoot.userData is Blackboard blackboard)
				{
					blackboard.RemoveObject(propertyName);
				}
			});

			IStyle removeButtonStyle = removeButton.style;
			removeButtonStyle.width = RemoveButtonWidth;
			removeButtonStyle.justifyContent = Justify.Center;

			GUIContent removeButtonIcon = EditorGUIUtility.IconContent(RemoveButtonIconContentName);
			if (removeButtonIcon != null && removeButtonIcon.image != null)
			{
				Texture image = removeButtonIcon.image;
				var removeButtonImage = new Image {image = image};
				IStyle removeButtonImageStyle = removeButtonImage.style;
				removeButtonImageStyle.height = image.height;
				removeButtonImageStyle.width = image.width;
				removeButton.Add(removeButtonImage);
			}
			else
			{
				removeButton.text = "x";
				removeButtonStyle.unityTextAlign = TextAnchor.MiddleCenter;
				removeButtonStyle.fontSize = 18;
			}

			return removeButton;
		}
	}
}
