# Simple Blackboard
Simple Blackboard for Unity is a flexible runtime data container that can contain any count of properties of any type.
The properties can be accessed with property names.

The main advantage of Simple Blackboard is that it allocates as little as possible.
Also, it has good enough performance and supports derivation.

## Installation

This repo is a regular Unity package. You can install it as your project dependency.
More here: https://docs.unity3d.com/Manual/upm-dependencies.html.

## Usage

### Setup

#### As a Unity component

Add a [SimpleBlackboardContainer] to a GameObject in your scene.
You can find it in **Add Component/Simple Blackboard/Simple Blackboard Container**.
That component automatically creates a [Blackboard] inside on Awake().
You can access it via `SimpleBlackboardContainer.blackboard` property.

#### As a regular c# class

Simply create a [Blackboard] with its default constructor: `new Blackboard()`.
You may get all the features of the Simple Blackboard this way too.


### [Blackboard] API

- `TryGetStructValue()` tries to get and return a value of a specified struct type and a property name.
- `TryGetClassValue()` tries to get and return a value of a specified class type and a property name.
- `TryGetObjectValue()` tries to get and return a value of any type and a property name.
- `SetStructValue()` sets a value of a specified struct type and a property name.
- `SetClassValue()` sets a value of a specified class type and a property name.
- `SetObjectValue()` sets a value of any type and a property name.
- `ContainsStructValue()` checks if a value of a struct type and a property name is contained.
- `ContainsObjectValue()` checks if a value of any type and a property name is contained.
- `ContainsType()` checks if a table of a type is contained.
- `GetCount()` returns how many values of a type are contained.
- `RemoveStruct()` removes a value of a struct type and a property name.
- `RemoveObject()` removes a value of any type and a property name.
- `Clear()` removes all values.

Those are main methods of [Blackboard], you can find more in its source code.

All the methods of [Blackboard] use [BlackboardPropertyName]
as a property name, not string. You can create that struct with one of its constructors. 
It transforms strings into unique integer ids. That makes work of [Blackboard] faster. 
But it's not recommended to create a new [BlackboardPropertyName] via string every time, cache it.

## Serialization

If you use a [Blackboard] as a regular c# class, you have to support serialization yourself.

If you use a [SimpleBlackboardContainer], you can create a [SimpleSerializedTablesContainer]
in **Assets/Create/Simple Blackboard/Simple Serialized Tables Container** 
and link it in your [SimpleBlackboardContainer].
[SimpleBlackboardContainer] also supports serialization of local components.
It will automatically apply all properties of [SimpleSerializedTablesContainer]s and references 
to local components on Awake().

### How to customize serialization

Although there are many types supported by Simple Blackboard out of the box, 
you may need to serialize more types or your own types.
In that case, you need to inherit [ClassGeneratedValueSerializedTable], [ClassSerializedValueSerializedTable],
[StructGeneratedValueSerializedTable], or [StructSerializedValueSerializedTable].

If you need a full customizable serialized table, you can derive [SerializedTable_Base].

If you need a full customizable serialized container, you can derive [SimpleSerializedContainer].

## Blackboard editor support

Because Unity draws only serialized by itself properties, 
properties of a [Blackboard] aren't drawn by Unity. 
But [SimpleBlackboardContainer] has a custom editor to show and make editable all of its [Blackboard] properties.
Although there are many types that are drawn out of the box, you may need to draw more types in Unity editor. 
You can achieve it by deriving [BlackboardValueView] or [UnityObjectBlackboardValueView].

If you have your own component and want to draw a blackboard in its inspector, use [BlackboardEditor].
It has a method `DrawBlackboard()` to draw a blackboard with IMGUI 
and methods `CreateBlackboardVisualElement()` and `UpdateBlackboardVisualElement()` to draw with UI Elements.

## Logs

There is a special class for logging the whole Blackboard system: [BlackboardDebug].
It contains conditional methods for logging. You can control a compilation of those methods with define symbols:
- SIMPLE_BLACKBOARD_LOG_DETAILS - log every change of the Blackboard system.
- SIMPLE_BLACKBOARD_LOG
- SIMPLE_BLACKBOARD_LOG_WARNING
- SIMPLE_BLACKBOARD_LOG_ERROR

[BlackboardDebug] has all of them as public const strings.

[Blackboard] may allocate in its methods new strings if you have any of those defines turned on.

## Multithreading

The package has an optional multithreading support. 
You can activate it with the define SIMPLE_BLACKBOARD_MULTITHREADING.
With that define, different locks are compiled. 
Those locks exist for internal usage only: access to [Blackboard]
in different classes of this package and internal work of [BlackboardPropertyName].
[Blackboard] doesn't have internal locks. So, you have to save your code from issues yourself.

Also, you can use the package in a multithreading code without the define SIMPLE_BLACKBOARD_MULTITHREADING.
In that case, you have to guarantee that you don't access the same [Blackboard]
from different threads and that you access the constructor with string 
and the name property of [BlackboardPropertyName] only from one thread.

[GlobalDefines] has the define SIMPLE_BLACKBOARD_MULTITHREADING as a public const string.

## Little features

### [BlackboardPropertyReference]

It's a simple serializable struct with two fields: a reference to [SimpleBlackboardContainer]
and a property name of string type. It has a custom editor that makes a selector for the property name field. 
The selector is filled with property names of all serialized properties 
of the referenced [SimpleBlackboardContainer].
It's recommended to use it for custom components 
where you need to reference a specific property in a [Blackboard] of a [SimpleBlackboardContainer].

### Accessors

These are special components that allow to easily get or set a value in a [Blackboard] 
in Unity component system.
You can find them in **Add Component/Simple Blackboard/Accessors/** or in the [AccessorsFolder].
You can get/set a blackboard property via the property `Accessor<T, TEvent>.value`.

Accessors also have Unity Event that is invoked with a current value in a blackboard. 
To flush such an event, you need to call the method `Accessor_Base.Flush()` 
or just use one of Accessor flushers that do that automatically.
You can find them in **Add Component/Simple Blackboard/Accessor Flushers/** or in the [AccessorFlushersFolder].

## See also
- [Event Based Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard) - 
another version of a blackboard system which flush events when its properties are changed 
but doesn't support derivation.

[Blackboard]: Runtime/Core/Blackboard.cs
[BlackboardPropertyName]: Runtime/Core/BlackboardPropertyName.cs
[SimpleBlackboardContainer]: Runtime/Components/SimpleBlackboardContainer.cs
[BlackboardPropertyReference]: Runtime/Components/BlackboardPropertyReference.cs
[SimpleSerializedContainer]: Runtime/Serialization/SimpleSerializedContainer.cs
[SimpleSerializedTablesContainer]: Runtime/Serialization/SimpleSerializedTablesContainer.cs
[SerializedTable_Base]: Runtime/Serialization/SerializedTables/SerializedTable_Base.cs
[ClassGeneratedValueSerializedTable]: Runtime/Serialization/SerializedTables/ClassGeneratedValueSerializedTable.cs
[ClassSerializedValueSerializedTable]: Runtime/Serialization/SerializedTables/ClassSerializedValueSerializedTable.cs
[StructGeneratedValueSerializedTable]: Runtime/Serialization/SerializedTables/StructGeneratedValueSerializedTable.cs
[StructSerializedValueSerializedTable]: Runtime/Serialization/SerializedTables/StructSerializedValueSerializedTable.cs
[BlackboardDebug]: Runtime/Debug/BlackboardDebug.cs
[BlackboardValueView]: Editor/ValueViews/BlackboardValueView.cs
[UnityObjectBlackboardValueView]: Editor/ValueViews/Implementations/UnityObjectBlackboardValueView.cs
[BlackboardEditor]: Editor/EditorTools/BlackboardEditor.cs
[GlobalDefines]: Runtime/GlobalDefines.cs
[AccessorsFolder]: Runtime/Components/Accessors
[AccessorFlushersFolder]: Runtime/Components/AccessorFlushers
