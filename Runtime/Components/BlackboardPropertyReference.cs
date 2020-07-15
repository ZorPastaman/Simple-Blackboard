// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;

namespace Zor.SimpleBlackboard.Components
{
	/// <summary>
	/// Useful serializable struct for referencing a property in <see cref="Zor.SimpleBlackboard.Core.Blackboard"/>
	/// in <see cref="Zor.SimpleBlackboard.Components.BlackboardContainer"/>.
	/// </summary>
	/// <remarks>
	/// Usually it's used in <see cref="UnityEngine.Component"/>.
	/// </remarks>
	[Serializable]
	public struct BlackboardPropertyReference
	{
#pragma warning disable CS0649
		public BlackboardContainer blackboardContainer;
		public string propertyName;
#pragma warning restore CS0649
	}
}
