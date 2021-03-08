// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using JetBrains.Annotations;

namespace Zor.SimpleBlackboard.Components
{
	/// <summary>
	/// Constants collection to use in <see cref="UnityEngine.AddComponentMenu"/>.
	/// </summary>
	public static class AddComponentConstants
	{
		[NotNull] public const string SimpleBlackboardFolder = "Simple Blackboard/";
		[NotNull] public const string AccessorsFolder = SimpleBlackboardFolder + "Accessors/";
		[NotNull] public const string AccessorFlushersFolder = SimpleBlackboardFolder + "Accessor Flushers/";
	}
}
