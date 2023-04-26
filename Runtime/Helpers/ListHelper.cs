// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.SimpleBlackboard.Helpers
{
	internal static class ListHelper
	{
		public static void EnsureCapacity<T>([NotNull] List<T> list, int additionalCount)
		{
			int requiredCapacity = list.Count + additionalCount;
			int currentCapacity = list.Capacity;

			if (currentCapacity >= requiredCapacity)
			{
				return;
			}

			int num = currentCapacity == 0 ? 4 : currentCapacity * 2;

			if ((uint)num > 2146435071U)
			{
				num = 2146435071;
			}

			if (num < requiredCapacity)
			{
				num = requiredCapacity;
			}

			list.Capacity = num;
		}
	}
}
