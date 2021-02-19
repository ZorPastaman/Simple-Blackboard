// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

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
		private const string ContainerElementName = "Container";
		private const string ChangePropertyElementName = "ChangedProperty";

		private static readonly List<KeyValuePair<BlackboardPropertyName, T>> s_properties =
			new List<KeyValuePair<BlackboardPropertyName, T>>();

		private readonly BlackboardValueView<T> m_blackboardValueView;

		/// <summary>
		/// Creates a <see cref="BlackboardTableEditor{T}"/> using <paramref name="blackboardValueView"/> for drawing.
		/// </summary>
		/// <param name="blackboardValueView">
		/// This is used for drawing a property in <see cref="BlackboardTable{T}"/>
		/// </param>
		protected BlackboardTableEditor(BlackboardValueView<T> blackboardValueView)
		{
			m_blackboardValueView = blackboardValueView;
		}

		/// <inheritdoc/>
		public override Type valueType => typeof(T);

		/// <inheritdoc/>
		public override void Draw(Blackboard blackboard)
		{
			try
			{
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
						s_RemoveButtonOptions))
					{
						blackboard.RemoveObject(key);
					}

					EditorGUILayout.EndHorizontal();
				}
			}
			finally
			{
				s_properties.Clear();
			}
		}

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

		public override void UpdateTable(VisualElement root, VisualElement blackboardRoot,
			Blackboard blackboard)
		{
			try
			{
				VisualElement container = root.Q(ContainerElementName);
				GetProperties(blackboard, s_properties);

				for (int i = container.childCount - 1; i >= 0; --i)
				{
					VisualElement propertyElement = container[i];

					if (!ContainsPropertyOfName(propertyElement.name))
					{
						container.RemoveAt(i);
					}
				}

				for (int i = 0, count = s_properties.Count; i < count; ++i)
				{
					KeyValuePair<BlackboardPropertyName, T> property = s_properties[i];
					BlackboardPropertyName key = property.Key;

					VisualElement propertyElement = container.Q(key.name)
						?? CreatePropertyElement(container, blackboardRoot, key);
					m_blackboardValueView.UpdateValue(propertyElement.Q(ChangePropertyElementName), property.Value);
				}

				container.Sort((left, right) => string.CompareOrdinal(left.name, right.name));
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
		/// with property name <paramref name="key"/>.
		/// </summary>
		/// <param name="blackboard">Property is set into this.</param>
		/// <param name="key">Property name.</param>
		/// <param name="value">Property value.</param>
		protected abstract void SetValue([NotNull] Blackboard blackboard,
			BlackboardPropertyName key, T value);

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

		[NotNull]
		private VisualElement CreatePropertyElement([NotNull] VisualElement container,
			[NotNull] VisualElement blackboardRoot, BlackboardPropertyName propertyName)
		{
			string keyName = propertyName.name;

			var propertyElement = new VisualElement {name = keyName};
			propertyElement.style.flexDirection = FlexDirection.Row;
			container.Add(propertyElement);

			VisualElement changePropertyElement = m_blackboardValueView.CreateVisualElement(keyName, blackboardRoot);
			changePropertyElement.name = ChangePropertyElementName;
			changePropertyElement.style.flexGrow = 1f;
			propertyElement.Add(changePropertyElement);

			Button removeButton = CreateRemoveButton(blackboardRoot, propertyName);
			propertyElement.Add(removeButton);

			return propertyElement;
		}

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
