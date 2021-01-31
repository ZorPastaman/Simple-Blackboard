// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Zor.SimpleBlackboard.Serialization;
using Object = UnityEngine.Object;

namespace Zor.SimpleBlackboard.EditorTools
{
	/// <summary>
	/// Static collection of serialized table types.
	/// </summary>
	[InitializeOnLoad]
	internal static class SerializedTableTypesCollection
	{
		private static readonly Type[] s_serializedTableTypes;
		private static readonly Type[] s_valueTypes;

		static SerializedTableTypesCollection()
		{
			s_serializedTableTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
					where !domainAssembly.IsDynamic
					from assemblyType in domainAssembly.GetExportedTypes()
					where !assemblyType.IsAbstract && !assemblyType.IsGenericType
						&& assemblyType.IsSubclassOf(typeof(SerializedTable_Base))
					select assemblyType)
				.ToArray();

			int count = s_serializedTableTypes.Length;
			s_valueTypes = new Type[count];

			for (int i = 0; i < count; ++i)
			{
				var tempSerializedTable =
					(SerializedTable_Base)ScriptableObject.CreateInstance(s_serializedTableTypes[i]);
				s_valueTypes[i] = tempSerializedTable.valueType;
				Object.DestroyImmediate(tempSerializedTable);
			}
		}

		/// <summary>
		/// Get all serialized table and value types.
		/// </summary>
		/// <param name="tableTypes">Table types are added to this.</param>
		/// <param name="valueTypes">Value types are added to this.</param>
		/// <param name="existingTables">
		/// <para>If this exists, it's used to filter the result - value types that exist in
		/// <paramref name="valueTypes"/> are not added.</para>
		/// <para>This has to be an array of <see cref="SerializedTable_Base"/>.</para>
		/// </param>
		/// <remarks>
		/// <paramref name="tableTypes"/> and <paramref name="valueTypes"/> are synchronized by index.
		/// </remarks>
		public static void GetSerializedTableTypes([NotNull] List<Type> tableTypes, [NotNull] List<Type> valueTypes,
			[CanBeNull] SerializedProperty existingTables)
		{
			for (int i = 0, count = s_serializedTableTypes.Length; i < count; ++i)
			{
				Type valueType = s_valueTypes[i];

				if (existingTables == null || !Contains(existingTables, valueType))
				{
					tableTypes.Add(s_serializedTableTypes[i]);
					valueTypes.Add(valueType);
				}
			}
		}

		private static bool Contains(SerializedProperty tables, Type type)
		{
			for (int i = 0, count = tables.arraySize; i < count; ++i)
			{
				Type valueType = ((SerializedTable_Base)tables.GetArrayElementAtIndex(i)
					.objectReferenceValue).valueType;

				if (valueType == type)
				{
					return true;
				}
			}

			return false;
		}
	}
}
