// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Zor.SimpleBlackboard.Debugging
{
	/// <summary>
	/// Class for logging the blackboard system.
	/// </summary>
	public static class BlackboardDebug
	{
		/// <summary>
		/// This define allows to log every change of the blackboard system. It can be too extensive. Use it only if
		/// you have problems in the blackboard system and don't understand an origin of them at all.
		/// </summary>
		public const string LogDetailsDefine = "SIMPLE_BLACKBOARD_LOG_DETAILS";
		public const string LogDefine = "SIMPLE_BLACKBOARD_LOG";
		public const string WarningDefine = "SIMPLE_BLACKBOARD_LOG_WARNING";
		public const string ErrorDefine = "SIMPLE_BLACKBOARD_LOG_ERROR";

		private const string Format = "[SimpleBlackboard] {0}.";

		[Conditional(LogDetailsDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogDetails(string message)
		{
			UnityEngine.Debug.LogFormat(Format, message);
		}

		[Conditional(LogDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Log(string message)
		{
			UnityEngine.Debug.LogFormat(Format, message);
		}

		[Conditional(WarningDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogWarning(string message)
		{
			UnityEngine.Debug.LogWarningFormat(Format, message);
		}

		[Conditional(ErrorDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogError(string message)
		{
			UnityEngine.Debug.LogErrorFormat(Format, message);
		}
	}
}
