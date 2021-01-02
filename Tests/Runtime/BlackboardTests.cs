// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Simple-Blackboard

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using Zor.SimpleBlackboard.Core;
using Object = UnityEngine.Object;

namespace Zor.SimpleBlackboard.Tests
{
	public static class BlackboardTests
	{
		[Test]
		public static void SetObjectTryGetObjectTests()
		{
			var blackboard = new Blackboard();

			SetObjectTryGetObjectTest(blackboard,
				new[]
					{
						new Mesh(), new Mesh(),
						(object)6, 8,
						new GameObject(), new GameObject(),
						new List<object>(), new List<object>()
					});
		}

		[Test]
		public static void SetStructTryGetStructTests()
		{
			var blackboard = new Blackboard();

			SetStructTryGetStructTest(blackboard,
				new[] { 5, 10, -56, 10000, 0, -15000 });
			SetStructTryGetStructTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void SetObjectTryGetClassTests()
		{
			var blackboard = new Blackboard();

			SetObjectTryGetClassTest<Object>(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new GameObject().AddComponent<Rigidbody>(), new GameObject().AddComponent<Rigidbody>()
				});
			SetObjectTryGetClassTest<ICollection>(blackboard,
				new object[]
				{
					new List<object>(), new List<object>(),
					new object[5], new object[5],
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			SetObjectTryGetClassTest<object>(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
		}

		[Test]
		public static void SetObjectTryGetObjectWithTypeTests()
		{
			var blackboard = new Blackboard();

			SetObjectTryGetObjectTest<Object>(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new GameObject().AddComponent<Rigidbody>(), new GameObject().AddComponent<Rigidbody>()
				});
			SetObjectTryGetObjectTest<ICollection>(blackboard,
				new object[]
				{
					new List<object>(), new List<object>(),
					new object[5], new object[5],
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			SetObjectTryGetObjectTest<object>(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			SetObjectTryGetObjectTest<int>(blackboard, new object[] { 5, -100, 300, 0 });
			SetObjectTryGetObjectTest<float>(blackboard,
				new object[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void GetStructPropertiesTests()
		{
			var blackboard = new Blackboard();

			GetStructPropertiesTest(blackboard,
				new[] { 5, 10, -56, 10000, 0, -15000 });
			GetStructPropertiesTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void GetClassPropertiesTests()
		{
			var blackboard = new Blackboard();

			GetClassPropertiesTest(blackboard,
				new Object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new GameObject().AddComponent<Rigidbody>(), new GameObject().AddComponent<Rigidbody>()
				});
			GetClassPropertiesTest(blackboard,
				new ICollection[]
				{
					new List<object>(), new List<object>(), new object[5], new object[5],
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			blackboard.Clear();
			GetClassPropertiesTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
		}

		[Test]
		public static void GetObjectPropertiesWithTypeTests()
		{
			var blackboard = new Blackboard();

			GetObjectPropertiesWithTypeTest(blackboard,
				new Object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new GameObject().AddComponent<Rigidbody>(), new GameObject().AddComponent<Rigidbody>()
				});
			GetObjectPropertiesWithTypeTest(blackboard,
				new ICollection[]
				{
					new List<object>(), new List<object>(), new object[5], new object[5],
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			blackboard.Clear();
			GetObjectPropertiesWithTypeTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			GetObjectPropertiesWithTypeTest(blackboard,
				new[] {5, 10, -56, 10000, 0, -15000});
			GetObjectPropertiesWithTypeTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void GetObjectPropertiesTests()
		{
			var blackboard = new Blackboard();

			GetObjectPropertiesTest(blackboard,
				new Object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new GameObject().AddComponent<Rigidbody>(), new GameObject().AddComponent<Rigidbody>()
				});
			blackboard.Clear();
			GetObjectPropertiesTest(blackboard,
				new ICollection[]
				{
					new List<object>(), new List<object>(), new object[5], new object[5],
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			blackboard.Clear();
			GetObjectPropertiesTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			blackboard.Clear();
			GetObjectPropertiesTest(blackboard,
				new[] {5, 10, -56, 10000, 0, -15000});
			blackboard.Clear();
			GetObjectPropertiesTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void GetTypeTests()
		{
			var blackboard = new Blackboard();

			GetTypeTest(blackboard, new[]
			{
				new GameObject(), new Mesh(), new GameObject().AddComponent<Rigidbody>(),
				new Dictionary<object, object>(), new List<object>(),
				new GameObject(), new Mesh(), new GameObject().AddComponent<Rigidbody>(),
				new Dictionary<object, object>(), new List<object>(),
				(object)56, 134, 56.67, -300.56, 100f, -80f
			});
		}

		[Test]
		public static void GetTypesTests()
		{
			var blackboard = new Blackboard();

			GetTypesTest(blackboard, new[]
			{
				new GameObject(), new Mesh(), new GameObject().AddComponent<Rigidbody>(),
				new Dictionary<object, object>(), new List<object>(),
				new GameObject(), new Mesh(), new GameObject().AddComponent<Rigidbody>(),
				new Dictionary<object, object>(), new List<object>(),
				(object)56, 134, 56.67, -300.56, 100f, -80f
			});
		}

		[Test]
		public static void GetPropertyNamesTests()
		{
			var blackboard = new Blackboard();

			GetPropertyNamesTest(blackboard, new[]
			{
				new GameObject(), new Mesh(), new GameObject().AddComponent<Rigidbody>(),
				new Dictionary<object, object>(), new List<object>(),
				new GameObject(), new Mesh(), new GameObject().AddComponent<Rigidbody>(),
				new Dictionary<object, object>(), new List<object>(),
				(object)56, 134, 56.67, -300.56, 100f, -80f
			});
		}

		[Test]
		public static void ContainsStructValueTests()
		{
			var blackboard = new Blackboard();

			ContainsStructValueTest(blackboard,
				new[] { 5, 10, -56, 10000, 0, -15000 });
			ContainsStructValueTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void ContainsObjectValueTests()
		{
			var blackboard = new Blackboard();

			ContainsObjectValueTest(blackboard,
				new Object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new GameObject().AddComponent<Rigidbody>(), new GameObject().AddComponent<Rigidbody>()
				});
			ContainsObjectValueTest(blackboard,
				new ICollection[]
				{
					new List<object>(), new List<object>(),
					new object[5], new object[5],
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			ContainsObjectValueTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			ContainsObjectValueTest(blackboard, new[] { 5, -100, 300, 0 });
			ContainsObjectValueTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void ContainsObjectValueWithTypeTests()
		{
			var blackboard = new Blackboard();

			ContainsObjectValueWithTypeTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new GameObject().AddComponent<Rigidbody>(), new GameObject().AddComponent<Rigidbody>()
				});
			ContainsObjectValueWithTypeTest(blackboard,
				new object[]
				{
					new List<object>(), new List<object>(),
					new object[5], new object[5],
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			ContainsObjectValueWithTypeTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			ContainsObjectValueWithTypeTest(blackboard, new object[] { 5, -100, 300, 0 });
			ContainsObjectValueWithTypeTest(blackboard,
				new object[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void ContainsObjectValueWithoutTypeTests()
		{
			var blackboard = new Blackboard();

			ContainsObjectValueWithoutTypeTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new GameObject().AddComponent<Rigidbody>(), new GameObject().AddComponent<Rigidbody>()
				});
			ContainsObjectValueWithoutTypeTest(blackboard,
				new object[]
				{
					new List<object>(), new List<object>(),
					new object[5], new object[5],
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			ContainsObjectValueWithoutTypeTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh(),
					new Dictionary<object, object>(), new Dictionary<object, object>()
				});
			ContainsObjectValueWithoutTypeTest(blackboard, new object[] { 5, -100, 300, 0 });
			ContainsObjectValueWithoutTypeTest(blackboard,
				new object[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void ContainsTypeTests()
		{
			var blackboard = new Blackboard();

			ContainsTypeTest(blackboard,
				new[]
				{
					new GameObject(), new GameObject()
				});
			ContainsTypeTest(blackboard,
				new[]
				{
					new List<object>(), new List<object>()
				});
			ContainsTypeTest(blackboard,
				new[]
				{
					new Mesh(), new Mesh()
				});
			ContainsTypeTest(blackboard, new[] { 5, -100, 300, 0 });
			ContainsTypeTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void ContainsInheritorTypeTests()
		{
			var blackboard = new Blackboard();

			ContainsInheritorTypeTest(blackboard,
				new Object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh()
				});
			ContainsInheritorTypeTest(blackboard,
				new IList[]
				{
					new List<object>(), new List<object>(),
					new object[1], new object[1]
				});
			ContainsInheritorTypeTest(blackboard,
				new Component[]
				{
					new GameObject().AddComponent<Rigidbody>(), new GameObject().transform
				});
			ContainsInheritorTypeTest(blackboard, new[] { 5, -100, 300, 0 });
			ContainsInheritorTypeTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void GetCountTests()
		{
			var blackboard = new Blackboard();

			GetCountTest(blackboard,
				new[]
				{
					new GameObject(), new GameObject()
				});
			GetCountTest(blackboard,
				new[]
				{
					new List<object>(), new List<object>()
				});
			GetCountTest(blackboard,
				new[]
				{
					new Mesh(), new Mesh()
				});
			GetCountTest(blackboard, new[] { 5, -100, 300, 0 });
			GetCountTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void GetCountWithInheritorsTests()
		{
			var blackboard = new Blackboard();

			GetCountWithInheritorsTest(blackboard,
				new Object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh()
				});
			GetCountWithInheritorsTest(blackboard,
				new IList[]
				{
					new List<object>(), new List<object>(),
					new object[1], new object[1]
				});
			GetCountWithInheritorsTest(blackboard,
				new Component[]
				{
					new GameObject().AddComponent<Rigidbody>(), new GameObject().transform
				});
			GetCountWithInheritorsTest(blackboard, new[] { 5, -100, 300, 0 });
			GetCountWithInheritorsTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void RemoveStructTests()
		{
			var blackboard = new Blackboard();

			RemoveStructTest(blackboard, new[] { 5, -100, 300, 0 });
			RemoveStructTest(blackboard, new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void RemoveObjectTests()
		{
			var blackboard = new Blackboard();

			RemoveObjectTest(blackboard,
				new Object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh()
				});
			RemoveObjectTest(blackboard,
				new IList[]
				{
					new List<object>(), new List<object>(),
					new object[1], new object[1]
				});
			RemoveObjectTest(blackboard,
				new Component[]
				{
					new GameObject().AddComponent<Rigidbody>(), new GameObject().transform
				});
			RemoveObjectTest(blackboard, new[] { 5, -100, 300, 0 });
			RemoveObjectTest(blackboard,
				new[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void RemoveObjectWithTypeTests()
		{
			var blackboard = new Blackboard();

			RemoveObjectWithTypeTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh()
				});
			RemoveObjectWithTypeTest(blackboard,
				new object[]
				{
					new List<object>(), new List<object>(),
					new object[1], new object[1]
				});
			RemoveObjectWithTypeTest(blackboard,
				new object[]
				{
					new GameObject().AddComponent<Rigidbody>(), new GameObject().transform
				});
			RemoveObjectWithTypeTest(blackboard, new object[] { 5, -100, 300, 0 });
			RemoveObjectWithTypeTest(blackboard,
				new object[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void RemoveObjectWithoutType()
		{
			var blackboard = new Blackboard();

			RemoveObjectTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh()
				});
			blackboard.Clear();
			RemoveObjectTest(blackboard,
				new object[]
				{
					new List<object>(), new List<object>(),
					new object[1], new object[1]
				});
			blackboard.Clear();
			RemoveObjectTest(blackboard,
				new object[]
				{
					new GameObject().AddComponent<Rigidbody>(), new GameObject().transform
				});
			blackboard.Clear();
			RemoveObjectTest(blackboard, new object[] { 5, -100, 300, 0 });
			blackboard.Clear();
			RemoveObjectTest(blackboard,
				new object[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void ClearTests()
		{
			var blackboard = new Blackboard();

			ClearTest(blackboard,
				new object[]
				{
					new GameObject(), new GameObject(),
					new Mesh(), new Mesh()
				});
			ClearTest(blackboard,
				new object[]
				{
					new List<object>(), new List<object>(),
					new object[1], new object[1]
				});
			ClearTest(blackboard,
				new object[]
				{
					new GameObject().AddComponent<Rigidbody>(), new GameObject().transform
				});
			ClearTest(blackboard, new object[] { 5, -100, 300, 0 });
			ClearTest(blackboard,
				new object[] { 5f, 10.76f, -56.55501f, 10000.8f, 0.5f, -15000.43f });
		}

		[Test]
		public static void ValueTypesCountTests()
		{
			var blackboard = new Blackboard();

			ValueTypesCountTest(blackboard, new object[]
			{
				new GameObject(), new GameObject(),
				new List<object>(), new List<int>(),
				new GameObject().AddComponent<Rigidbody>(), new GameObject().transform,
			});

			blackboard = new Blackboard();
			ValueTypesCountTest(blackboard, new object[]
			{
				new int[1], new float[1],
				new Dictionary<int, int>(), new List()
			});
		}

		[Test]
		public static void PropertiesCountTests()
		{
			var blackboard = new Blackboard();

			PropertiesCountTest(blackboard, new object[]
			{
				new GameObject(), new GameObject(),
				new List<object>(), new List<int>(),
				new GameObject().AddComponent<Rigidbody>(), new GameObject().transform,
				1, 230f
			});
			blackboard.Clear();
			PropertiesCountTest(blackboard, new object[]
			{
				new int[1], new float[1],
				new Dictionary<int, int>(), new List(),
				12, 230.87f
			});
		}

		[Test]
		public static void CopyToTests()
		{
			var fromBlackboard = new Blackboard();
			var toBlackboard = new Blackboard();

			CopyToTest(fromBlackboard, toBlackboard, new object[]
			{
				new GameObject(), new GameObject(),
				new List<object>(), new List<int>(),
				new GameObject().AddComponent<Rigidbody>(), new GameObject().transform,
				1, 230f
			});

			CopyToTest(fromBlackboard, toBlackboard, new object[]
			{
				new int[1], new float[1],
				new Dictionary<int, int>(), new List(),
				12, 230.87f
			});

			CopyToTest(fromBlackboard, toBlackboard, new object[]
			{
				new GameObject(), new GameObject(),
				new List<object>(), new List<int>(),
				new GameObject().AddComponent<Rigidbody>(), new GameObject().transform,
				1, 230f
			});

			CopyToTest(fromBlackboard, toBlackboard, new GameObject());
			CopyToTest(fromBlackboard, toBlackboard, 1);
			CopyToTest(fromBlackboard, toBlackboard, new List<object>());
		}

		[Test]
		public static void MultithreadingTests()
		{
			var blackboard = new Blackboard();
			var copiedBlackboard = new Blackboard();

			void ThreadOperation()
			{
				var structProperty = new BlackboardPropertyName("struct");
				var classProperty = new BlackboardPropertyName("class");
				var objectProperty = new BlackboardPropertyName("object");

				lock (blackboard)
				{
					blackboard.SetStructValue(structProperty, 3);
				}
				lock (blackboard)
				{
					blackboard.SetClassValue(classProperty, new List<int>());
				}
				lock (blackboard)
				{
					blackboard.SetObjectValue(typeof(object), objectProperty, new object());
				}

				lock (blackboard)
				{
					blackboard.TryGetStructValue(structProperty, out int structValue);
				}
				lock (blackboard)
				{
					blackboard.TryGetClassValue(classProperty, out object classValue);
				}
				lock (blackboard)
				{
					blackboard.TryGetObjectValue(objectProperty, out object objectValue);
				}

				var structProperties = new List<KeyValuePair<BlackboardPropertyName, int>>();
				lock (blackboard)
				{
					blackboard.GetStructProperties(structProperties);
				}
				var classProperties = new List<KeyValuePair<BlackboardPropertyName, List<int>>>();
				lock (blackboard)
				{
					blackboard.GetClassProperties(classProperties);
				}
				var objectProperties = new List<KeyValuePair<BlackboardPropertyName, object>>();

				lock (blackboard)
				{
					blackboard.GetObjectProperties(objectProperties);
				}

				lock (blackboard)
				{
					blackboard.GetValueType(structProperty);
				}

				var valueTypes = new List<Type>();
				lock (blackboard)
				{
					blackboard.GetValueTypes(valueTypes);
				}

				var propertyNames = new List<BlackboardPropertyName>();
				lock (blackboard)
				{
					blackboard.GetPropertyNames(propertyNames);
				}

				lock (blackboard)
				{
					blackboard.ContainsStructValue<int>(structProperty);
				}
				lock (blackboard)
				{
					blackboard.ContainsObjectValue<object>(objectProperty);
				}
				lock (blackboard)
				{
					blackboard.ContainsObjectValue(typeof(object), objectProperty);
				}
				lock (blackboard)
				{
					blackboard.ContainsObjectValue(classProperty);
				}
				lock (blackboard)
				{
					blackboard.ContainsType<List<int>>();
				}
				lock (blackboard)
				{
					blackboard.ContainsType(typeof(List<int>));
				}
				lock (blackboard)
				{
					blackboard.ContainsInheritingType<object>();
				}
				lock (blackboard)
				{
					blackboard.ContainsInheritingType(typeof(object));
				}

				lock (blackboard)
				{
					blackboard.GetCount<int>();
				}
				lock (blackboard)
				{
					blackboard.GetCount(typeof(int));
				}
				lock (blackboard)
				{
					blackboard.GetCountWithInheritors<object>();
				}
				lock (blackboard)
				{
					blackboard.GetCountWithInheritors(typeof(object));
				}

				lock (blackboard)
				lock (copiedBlackboard)
				{
					blackboard.CopyTo(copiedBlackboard);
				}

				lock (blackboard)
				{
					blackboard.RemoveStruct<int>(structProperty);
				}
				lock (blackboard)
				{
					blackboard.RemoveObject<List<int>>(classProperty);
				}
				lock (blackboard)
				{
					blackboard.RemoveObject(objectProperty);
				}

				lock (copiedBlackboard)
				{
					copiedBlackboard.RemoveObject(typeof(object), objectProperty);
				}

				lock (copiedBlackboard)
				{
					copiedBlackboard.Clear();
				}
			}

			var thread0 = new Thread(ThreadOperation);
			var thread1 = new Thread(ThreadOperation);
			thread0.Start();
			thread1.Start();

			thread0.Join();
			thread1.Join();
		}

		private static void SetObjectTryGetObjectTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetClassValue(propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Assert.IsTrue(blackboard.TryGetObjectValue(propertyNames[i], out object containedValue),
					$"Blackboard doesn't contain value of '{propertyNames[i].ToString()}'");
				Assert.AreEqual(values[i], containedValue,
					$"Value of '{propertyNames[i].ToString()}' in blackboard is not equal");
			}
		}

		private static void SetStructTryGetStructTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
			where T : struct
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetStructValue(propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Assert.IsTrue(blackboard.TryGetStructValue(propertyNames[i], out T containedValue),
					$"Blackboard doesn't contain value of '{propertyNames[i].ToString()}'");
				Assert.AreEqual(values[i], containedValue,
					$"Value of '{propertyNames[i].ToString()}' in blackboard is not equal");
			}
		}

		private static void SetObjectTryGetClassTest<TTryGet>([NotNull] Blackboard blackboard,
			[NotNull] object[] values) where TTryGet : class
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetClassValue(propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Assert.IsTrue(blackboard.TryGetClassValue(propertyNames[i], out TTryGet containedValue),
					$"Blackboard doesn't contain value of '{propertyNames[i].ToString()}' and '{typeof(TTryGet).FullName}'");
				Assert.AreEqual(values[i], containedValue,
					$"Value of '{propertyNames[i].ToString()}' in blackboard is not equal and '{typeof(TTryGet).FullName}'");
			}
		}

		private static void SetObjectTryGetObjectTest<TTryGet>([NotNull] Blackboard blackboard,
			[NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetClassValue(propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Assert.IsTrue(blackboard.TryGetObjectValue(typeof(TTryGet), propertyNames[i], out object containedValue),
					$"Blackboard doesn't contain value of '{propertyNames[i].ToString()}' and '{typeof(TTryGet).FullName}'");
				Assert.AreEqual(values[i], containedValue,
					$"Value of '{propertyNames[i].ToString()}' in blackboard is not equal and '{typeof(TTryGet).FullName}'");
			}
		}

		private static void GetStructPropertiesTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
			where T : struct
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetStructValue(propertyNames[i], values[i]);
			}

			var properties = new List<KeyValuePair<BlackboardPropertyName, T>>();
			blackboard.GetStructProperties(properties);

			for (int i = 0; i < count; ++i)
			{
				var property = new KeyValuePair<BlackboardPropertyName, T>(propertyNames[i], values[i]);
				Assert.IsTrue(properties.Contains(property),
					$"Blackboard doesn't contain value '{property.Value}' of name '{property.Key.ToString()}'");
			}
		}

		private static void GetClassPropertiesTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
			where T : class
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetClassValue(propertyNames[i], values[i]);
			}

			var properties = new List<KeyValuePair<BlackboardPropertyName, T>>();
			blackboard.GetClassProperties(properties);

			for (int i = 0; i < count; ++i)
			{
				var property = new KeyValuePair<BlackboardPropertyName, T>(propertyNames[i], values[i]);
				Assert.IsTrue(properties.Contains(property),
					$"Blackboard doesn't contain value '{property.Value}' of name '{property.Key.ToString()}'");
			}
		}

		private static void GetObjectPropertiesWithTypeTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(typeof(T), propertyNames[i], values[i]);
			}

			var properties = new List<KeyValuePair<BlackboardPropertyName, object>>();
			blackboard.GetObjectProperties(typeof(T), properties);

			for (int i = 0; i < count; ++i)
			{
				var property = new KeyValuePair<BlackboardPropertyName, object>(propertyNames[i], values[i]);
				Assert.IsTrue(properties.Contains(property),
					$"Blackboard doesn't contain value '{property.Value}' of name '{property.Key.ToString()}'");
			}
		}

		private static void GetObjectPropertiesTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(typeof(T), propertyNames[i], values[i]);
			}

			var properties = new List<KeyValuePair<BlackboardPropertyName, object>>();
			blackboard.GetObjectProperties(properties);

			for (int i = 0; i < count; ++i)
			{
				var property = new KeyValuePair<BlackboardPropertyName, object>(propertyNames[i], values[i]);
				Assert.IsTrue(properties.Contains(property),
					$"Blackboard doesn't contain value '{property.Value}' of name '{property.Key.ToString()}'");
			}
		}

		private static void GetTypeTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetClassValue(propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Type expectedType = values[i].GetType();
				Type gottenType = blackboard.GetValueType(propertyName);
				Assert.AreEqual(expectedType, gottenType,
					$"Blackboard has a wrong type for the property '{propertyName.ToString()}': '{gottenType.FullName}' instead of '{expectedType.FullName}'");
			}
		}

		private static void GetTypesTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetClassValue(propertyNames[i], values[i]);
			}

			var types = new List<Type>();
			blackboard.GetValueTypes(types);

			for (int i = 0; i < count; ++i)
			{
				Type type = values[i].GetType();
				Assert.IsTrue(types.Contains(type),
					$"Blackboard doesn't contain type '{type.FullName}'");
			}
		}

		private static void GetPropertyNamesTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetClassValue(propertyNames[i], values[i]);
			}

			var gottenPropertyNames = new List<BlackboardPropertyName>();
			blackboard.GetPropertyNames(gottenPropertyNames);

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsTrue(gottenPropertyNames.Contains(propertyName),
					$"Blackboard doesn't contain property name '{propertyName.ToString()}'");
			}

			var types = new List<Type>();
			blackboard.GetValueTypes(types);

			for (int i = 0; i < count; ++i)
			{
				Type type = values[i].GetType();
				Assert.IsTrue(types.Contains(type),
					$"Blackboard doesn't contain type '{type.FullName}'");
			}
		}

		private static void ContainsStructValueTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
			where T : struct
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetStructValue(propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Assert.IsTrue(blackboard.ContainsStructValue<T>(propertyNames[i]),
					$"Blackboard doesn't contain value of '{propertyNames[i].ToString()}'");
			}
		}

		private static void ContainsObjectValueTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(typeof(T), propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Assert.IsTrue(blackboard.ContainsObjectValue<T>(propertyNames[i]),
					$"Blackboard doesn't contain value of '{propertyNames[i].ToString()}'");
			}
		}

		private static void ContainsObjectValueWithTypeTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(values[i].GetType(), propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Assert.IsTrue(blackboard.ContainsObjectValue(values[i].GetType(), propertyNames[i]),
					$"Blackboard doesn't contain value of '{propertyNames[i].ToString()}'");
			}
		}

		private static void ContainsObjectValueWithoutTypeTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(values[i].GetType(), propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Assert.IsTrue(blackboard.ContainsObjectValue(propertyNames[i]),
					$"Blackboard doesn't contain value of '{propertyNames[i].ToString()}'");
			}
		}

		private static void ContainsTypeTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(typeof(T), propertyNames[i], values[i]);
			}

			Assert.IsTrue(blackboard.ContainsType<T>(),
				$"Blackboard doesn't contain type '{typeof(T).FullName}'. Generic");
			Assert.IsTrue(blackboard.ContainsType(typeof(T)),
				$"Blackboard doesn't contain type '{typeof(T).FullName}'. Non-Generic");
		}

		private static void ContainsInheritorTypeTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(typeof(T), propertyNames[i], values[i]);
			}

			Assert.IsTrue(blackboard.ContainsInheritingType<T>(),
				$"Blackboard doesn't contain inheritor type '{typeof(T).FullName}'. Generic");
			Assert.IsTrue(blackboard.ContainsInheritingType(typeof(T)),
				$"Blackboard doesn't contain inheritor type '{typeof(T).FullName}'. Non-Generic");
		}

		private static void GetCountTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(typeof(T), propertyNames[i], values[i]);
			}

			Assert.AreEqual(values.Length, blackboard.GetCount<T>(),
				$"Blackboard doesn't contain expected count of '{typeof(T).FullName}'. Generic");
			Assert.AreEqual(values.Length, blackboard.GetCount(typeof(T)),
				$"Blackboard doesn't contain expected count of '{typeof(T).FullName}'. Non-Generic");
		}

		private static void GetCountWithInheritorsTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(typeof(T), propertyNames[i], values[i]);
			}

			Assert.AreEqual(values.Length, blackboard.GetCountWithInheritors<T>(),
				$"Blackboard doesn't contain expected count of '{typeof(T).FullName}'. Generic");
			Assert.AreEqual(values.Length, blackboard.GetCountWithInheritors(typeof(T)),
				$"Blackboard doesn't contain expected count of '{typeof(T).FullName}'. Non-Generic");
		}

		private static void RemoveStructTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
			where T : struct
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetStructValue(propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsTrue(blackboard.RemoveStruct<T>(propertyName),
					$"Blackboard returned false on remove of '{propertyName}' of type '{typeof(T).FullName}'");
			}

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsFalse(blackboard.RemoveStruct<T>(propertyName),
					$"Blackboard returned true on impossible remove of '{propertyName}' of type '{typeof(T).FullName}'");
			}

			Assert.AreEqual(0, blackboard.GetCount<T>(),
				$"Blackboard contains removed values of type '{typeof(T).FullName}'");
		}

		private static void RemoveObjectTest<T>([NotNull] Blackboard blackboard, [NotNull] T[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(typeof(T), propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsTrue(blackboard.RemoveObject<T>(propertyName),
					$"Blackboard returned false on remove of '{propertyName}' of type '{typeof(T).FullName}'");
			}

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsFalse(blackboard.RemoveObject<T>(propertyName),
					$"Blackboard returned true on impossible remove of '{propertyName}' of type '{typeof(T).FullName}'");
			}

			Assert.AreEqual(0, blackboard.GetCountWithInheritors<T>(),
				$"Blackboard contains removed values of type '{typeof(T).FullName}'");
		}

		private static void RemoveObjectWithTypeTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(values[i].GetType(), propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				Type type = values[i].GetType();
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsTrue(blackboard.RemoveObject(type, propertyName),
					$"Blackboard returned false on remove of '{propertyName}' of type '{type.FullName}'");
			}

			for (int i = 0; i < count; ++i)
			{
				Type type = values[i].GetType();
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsFalse(blackboard.RemoveObject(type, propertyName),
					$"Blackboard returned true on impossible remove of '{propertyName}' of type '{type.FullName}'");
			}

			Assert.AreEqual(0, blackboard.GetCountWithInheritors(typeof(object)),
				$"Blackboard contains removed values");
		}

		private static void RemoveObjectTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(values[i].GetType(), propertyNames[i], values[i]);
			}

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsTrue(blackboard.RemoveObject(propertyName),
					$"Blackboard returned false on remove of '{propertyName}'");
			}

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsFalse(blackboard.RemoveObject(propertyName),
					$"Blackboard returned true on impossible remove of '{propertyName}'");
			}

			Assert.AreEqual(0, blackboard.GetCountWithInheritors(typeof(object)),
				$"Blackboard contains removed values");
		}

		private static void ClearTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(values[i].GetType(), propertyNames[i], values[i]);
			}

			blackboard.Clear();

			Assert.AreEqual(0, blackboard.propertiesCount,
				"Blackboard has something after clear");
		}

		private static void ValueTypesCountTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(values[i].GetType(), propertyNames[i], values[i]);
			}

			var types = new List<Type>();

			for (int i = 0; i < count; ++i)
			{
				Type type = values[i].GetType();

				if (!types.Contains(type))
				{
					types.Add(type);
				}
			}

			Assert.AreEqual(types.Count, blackboard.valueTypesCount,
				"Blackboard has wrong number of value types");
		}

		private static void PropertiesCountTest([NotNull] Blackboard blackboard, [NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				blackboard.SetObjectValue(values[i].GetType(), propertyNames[i], values[i]);
			}

			Assert.AreEqual(propertyNames.Length, blackboard.propertiesCount,
				"Blackboard has wrong number of properties");
		}

		private static void CopyToTest([NotNull] Blackboard fromBlackboard, [NotNull] Blackboard toBlackboard,
			[NotNull] object[] values)
		{
			int count = values.Length;
			var propertyNames = new BlackboardPropertyName[count];

			for (int i = 0; i < count; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName(values[i].GetType().FullName + " " + i.ToString());
			}

			for (int i = 0; i < count; ++i)
			{
				fromBlackboard.SetClassValue(propertyNames[i], values[i]);
			}

			fromBlackboard.CopyTo(toBlackboard);

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsTrue(toBlackboard.TryGetObjectValue(propertyName, out object value)
					&& value.Equals(values[i]),
					$"Blackboard has a wrong property of name {propertyName.ToString()}");
			}

			toBlackboard.Clear();
			fromBlackboard.CopyTo(toBlackboard, propertyNames);

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsTrue(toBlackboard.TryGetObjectValue(propertyName, out object value)
					&& value.Equals(values[i]),
					$"Blackboard has a wrong property of name {propertyName.ToString()}");
			}

			toBlackboard.Clear();
			var list = new List<BlackboardPropertyName>(propertyNames);
			fromBlackboard.CopyTo(toBlackboard, list);

			for (int i = 0; i < count; ++i)
			{
				BlackboardPropertyName propertyName = propertyNames[i];
				Assert.IsTrue(toBlackboard.TryGetObjectValue(propertyName, out object value)
					&& value.Equals(values[i]),
					$"Blackboard has a wrong property of name {propertyName.ToString()}");
			}
		}

		private static void CopyToTest([NotNull] Blackboard fromBlackboard, [NotNull] Blackboard toBlackboard,
			[NotNull] object value)
		{
			var propertyName = new BlackboardPropertyName(value.GetType().FullName + "");
			fromBlackboard.SetClassValue(propertyName, value);
			fromBlackboard.CopyTo(toBlackboard, propertyName);
			Assert.IsTrue(toBlackboard.TryGetClassValue(propertyName, out object containedValue)
				&& value.Equals(containedValue),
				$"Blackboard has a wrong property of name {propertyName.ToString()}");
		}
	}
}

