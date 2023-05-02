// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Zor.SimpleBlackboard.Core
{
	/// <summary>
	/// Wrapper over <see cref="string"/> transforming it into <see cref="int"/> for faster comparisons.
	/// </summary>
	/// <remarks>
	/// It's used in <see cref="Zor.SimpleBlackboard.Core.Blackboard"/>.
	/// </remarks>
	public readonly struct BlackboardPropertyName : IEquatable<BlackboardPropertyName>
	{
		/// <summary>
		/// 0 on little endian machines, 1 on big endian machines.
		/// Used in <see cref="ComputeHash"/>.
		/// </summary>
		private static readonly int s_hashByteOffset;

#if SIMPLE_BLACKBOARD_SAVE_NAMES
		/// <summary>
		/// Pairs of ids and strings that were used in <see cref="BlackboardPropertyName(string)"/>.
		/// </summary>
		[NotNull] private static readonly System.Collections.Generic.Dictionary<int, string> s_names = new(1000);
#endif

		/// <summary>
		/// Unique per string id.
		/// </summary>
		public readonly int id;

		/// <summary>
		/// Initializes <see cref="s_hashByteOffset"/>.
		/// </summary>
		static unsafe BlackboardPropertyName()
		{
			bool isBigEndian = !BitConverter.IsLittleEndian;
			s_hashByteOffset = *(byte*)&isBigEndian;
		}

		/// <summary>
		/// Creates a <see cref="BlackboardPropertyName"/> with unique <see cref="id"/> per <paramref name="name"/>.
		/// </summary>
		/// <param name="name">
		/// For this, unique <see cref="id"/> is set.
		/// Every symbol must be from ascii table.
		/// </param>
		public BlackboardPropertyName([NotNull] string name)
		{
			id = ComputeHash(name);

#if SIMPLE_BLACKBOARD_SAVE_NAMES
#if SIMPLE_BLACKBOARD_MULTITHREADING
			lock (s_names)
#endif
			{
				s_names[id] = name;
			}
#endif
		}

		/// <summary>
		/// Creates a <see cref="BlackboardPropertyName"/> with the specified <paramref name="id"/>.
		/// </summary>
		/// <param name="id">Id to set.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BlackboardPropertyName(int id)
		{
			this.id = id;
		}

		/// <summary>
		/// Name of the property. This method requires SIMPLE_BLACKBOARD_SAVE_NAMES define enabled.
		/// </summary>
		/// <returns>
		/// <para>
		/// Original string name of the property if the <see cref="BlackboardPropertyName"/> was created
		/// with <see cref="BlackboardPropertyName(string)"/>.
		/// </para>
		/// <para>
		/// If the <see cref="BlackboardPropertyName"/> was created with <see cref="BlackboardPropertyName(int)"/>,
		/// this may return <see cref="string.Empty"/> or a name if another <see cref="BlackboardPropertyName"/>
		/// was created with <see cref="BlackboardPropertyName(string)"/> and got the same <see cref="id"/>.
		/// </para>
		/// <para>
		/// Empty string if SIMPLE_BLACKBOARD_SAVE_NAMES define is not enabled.
		/// </para>
		/// </returns>
		[NotNull]
		public string name
		{
			[Pure]
			get
			{
#if SIMPLE_BLACKBOARD_SAVE_NAMES
#if SIMPLE_BLACKBOARD_MULTITHREADING
				lock (s_names)
#endif
				{
					return s_names.TryGetValue(id, out string propertyName) ? propertyName : string.Empty;
				}
#else
				return string.Empty;
#endif
			}
		}

		[Pure]
		public override bool Equals(object obj)
		{
			return obj is BlackboardPropertyName other && other.id == id;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool Equals(BlackboardPropertyName other)
		{
			return other.id == id;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override int GetHashCode()
		{
			return id;
		}

		[Pure]
		public override string ToString()
		{
#if SIMPLE_BLACKBOARD_SAVE_NAMES
			return $"{id.ToString()}({name})";
#else
			return id.ToString();
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static bool operator ==(BlackboardPropertyName lhs, BlackboardPropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static bool operator !=(BlackboardPropertyName lhs, BlackboardPropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		/// <summary>
		/// Computes hash for <see cref="str"/>. It uses FNV-1a hash algorithm.
		/// </summary>
		/// <param name="str">String to be hashed.</param>
		/// <returns>Computed hash.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		private static unsafe int ComputeHash([NotNull] string str)
		{
			fixed (char* c = str)
			{
				uint hash = 2166136261;

				for (byte* b = (byte*)c + s_hashByteOffset; *b != 0; b += 2)
				{
					hash = (hash ^ *b) * 16777619;
				}

				return unchecked((int)hash);
			}
		}
	}
}
