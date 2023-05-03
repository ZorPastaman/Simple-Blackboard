// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

namespace Zor.SimpleBlackboard
{
	/// <summary>
	/// Here are defines that affect the work of the whole package.
	/// </summary>
	public static class GlobalDefines
	{
		/// <summary>
		/// With this define, different locks are compiled.
		/// </summary>
		public const string MultithreadingDefine = "SIMPLE_BLACKBOARD_MULTITHREADING";

		/// <summary>
		/// With this define, original names of <see cref="Zor.SimpleBlackboard.Core.BlackboardPropertyName"/>
		/// are saved.
		/// </summary>
		public const string SaveNamesDefine = "SIMPLE_BLACKBOARD_SAVE_NAMES";
	}
}
