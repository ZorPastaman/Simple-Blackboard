// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <summary>
	/// Base class for <see cref="BlackboardTableEditor{T}"/>.
	/// </summary>
	internal abstract class BlackboardTableEditor_Base
	{
		[NotNull] protected const string RemoveButtonIconContentName = "TreeEditor.Trash";
		protected const float RemoveButtonWidth = 32f;
		[NotNull]
		protected static readonly GUILayoutOption[] RemoveButtonOptions = { GUILayout.Width(RemoveButtonWidth) };

		/// <summary>
		/// Value type of the drawn property.
		/// </summary>
		[NotNull]
		public abstract Type valueType { [Pure] get; }

		/// <summary>
		/// Draws an editor for <see cref="BlackboardTable{T}"/> in <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">For its <see cref="BlackboardTable{T}"/> the editor is drawn.</param>
		public abstract void Draw([NotNull] Blackboard blackboard);

		/// <summary>
		/// Creates a <see cref="VisualElement"/> for <see cref="BlackboardTable{T}"/>.
		/// </summary>
		/// <returns>View template for a <see cref="BlackboardTable{T}"/>.</returns>
		/// <remarks>
		/// <para>The returned <see cref="VisualElement"/> is used in <see cref="UpdateTable"/>.</para>
		/// <para>Do not modify the returned <see cref="VisualElement"/>.</para>
		/// </remarks>
		[NotNull]
		public abstract VisualElement CreateTable();

		/// <summary>
		/// Updates the <paramref name="tableRoot"/>.
		/// </summary>
		/// <param name="tableRoot">View template for a <see cref="BlackboardTable{T}"/>.</param>
		/// <param name="blackboardRoot">
		/// View template for a <see cref="Blackboard"/> that <paramref name="tableRoot"/> belongs to.
		/// </param>
		/// <remarks>
		/// <para><paramref name="tableRoot"/> must be created in <see cref="CreateTable"/>.</para>
		/// <para><paramref name="blackboardRoot"/> must have a link to its <see cref="Blackboard"/> in userData.</para>
		/// </remarks>
		public abstract void UpdateTable([NotNull] VisualElement tableRoot, [NotNull] VisualElement blackboardRoot);
	}
}
