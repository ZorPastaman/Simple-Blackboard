// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Profiling;
using Zor.SimpleBlackboard.Components;
using Zor.SimpleBlackboard.Core;

namespace Zor.SimpleBlackboard.Tests
{
	public sealed class BlackboardProfiling : MonoBehaviour
	{
		private const int ArrayLength = 100;

#pragma warning disable CS0649
		[SerializeField] private SimpleBlackboardContainer m_BlackboardContainer;
		[SerializeField] private bool m_NewPropertyNamePerValue;
#pragma warning restore CS0649

		private Blackboard m_blackboard;

		private BlackboardPropertyName[] m_propertyNames;

		private int[] m_ints;
		private double[] m_doubles;
		private float[] m_floats;
		private short[] m_shorts;

		private Mesh[] m_meshes;

		private int m_step;

		private void Start()
		{
			m_blackboard = m_BlackboardContainer.blackboard;

			m_propertyNames = CreateArray(new BlackboardPropertyName("0"),
				(value, index) => new BlackboardPropertyName(index.ToString()));

			m_ints = CreateArray(-ArrayLength / 2, (value, index) => value + 1);
			m_doubles = CreateArray(-(double)ArrayLength / 2, (value, index) => value + 1.0);
			m_floats = CreateArray(-(float)ArrayLength / 2, (value, index) => value + 1f);
			m_shorts = CreateArray((short)(-ArrayLength / 2), (value, index) => (short)(value + 1));

			m_meshes = CreateArray(new Mesh(), (value, index) => new Mesh());
		}

		private void Update()
		{
			SetStructValues(m_ints);
			SetStructValues(m_doubles);
			SetStructValues(m_floats);
			SetStructValues(m_shorts);
			SetClassValues(m_meshes);

			var thread = new Thread(() =>
			{
				Profiler.BeginThreadProfiling("Blackboard", "Blackboard Thread Profiler");

				SetStructValues(m_ints);
				SetStructValues(m_doubles);
				SetStructValues(m_floats);
				SetStructValues(m_shorts);

				Profiler.EndThreadProfiling();
			});
			thread.Start();
			thread.Join();

			m_step++;
		}

		private void SetStructValues<T>(T[] array) where T : struct
		{
			for (int i = 0; i < ArrayLength; ++i)
			{
				int index = m_NewPropertyNamePerValue ? m_step + i : m_step;
				BlackboardPropertyName propertyName = m_propertyNames[index % ArrayLength];

				m_blackboard.SetStructValue(propertyName, array[i]);
			}
		}

		private void SetClassValues<T>(T[] array) where T : class
		{
			for (int i = 0; i < ArrayLength; ++i)
			{
				int index = m_NewPropertyNamePerValue ? m_step + i : m_step;
				BlackboardPropertyName propertyName = m_propertyNames[index % ArrayLength];

				m_blackboard.SetClassValue(propertyName, array[i]);
			}
		}

		private static T[] CreateArray<T>(T initialValue, Func<T, int, T> getNext)
		{
			var array = new T[ArrayLength];
			array[0] = initialValue;

			for (int i = 1; i < ArrayLength; ++i)
			{
				array[i] = getNext(array[i - 1], i);
			}

			return array;
		}

		private void Reset()
		{
			m_blackboard = new Blackboard();
		}

		[ContextMenu("Log")]
		private void Log()
		{
			UnityEngine.Debug.Log(m_blackboard.ToString());
		}
	}
}
