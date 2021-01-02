# Simple Blackboard
Simple Blackboard for Unity is a flexible data storage that can contain any count of properties of any type.
The properties can be accessed with property names.

The main advantage of Simple Blackboard is that it allocates as little as possible.
Also it has good enough performance and supports derivation.

## Installation

This repo is a regular Unity package. You can install it as your project dependency.
More here: https://docs.unity3d.com/Manual/upm-dependencies.html.

## Usage

### Setup

#### As a Unity component

Add a [BlackboardContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardContainer.cs)
to a GameObject in your scene.
You can find it in **Add Component/Simple Blackboard/Blackboard Container**.
That component automatically creates a [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) inside.
You can access it via `BlackboardContainer.blackboard` property.

#### As a regular c# class

Simply create a [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) with its default constructor: `new Blackboard()`.
You may get all the features of the Simple Blackboard this way too.


### [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) API

- `TryGetStructValue()` tries to get and return a value of a specified struct type and property name.
- `TryGetClassValue()` tries to get and return a value of a specified class type and property name.
- `SetStructValue()` sets a value of a specified struct type and property name.
- `SetClassValue()` sets a value of a specified class type and property name.

Those are main methods of [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs),
you can find more in its source code.

All the methods of [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
use [BlackboardPropertyName](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/BlackboardPropertyName.cs)
as a property name, not string. You can create that struct with one of its constructors. It transforms strings into unique integer ids. That makes work of
[Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) faster. But it's not recommended to create a new
[BlackboardPropertyName](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/BlackboardPropertyName.cs) every time, cache it.

## Serialization

If you use a [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) as a regular c# class,
you have to support serialization yourself.

If you use a [BlackboardContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardContainer.cs),
you can create a [SerializedTablesContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Serialization/SerializedTablesContainer.cs)
in **Assets/Create/Simple Blackboard/Serialized Tables Container** and link it in your
[BlackboardContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardContainer.cs).
[BlackboardContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardContainer.cs)
also supports serialization of local components.
It will automatically apply all properties of Serialized Tables Containers and references to local components on Awake().

### How to customize serialization

Although there are many types supported by Simple Blackboard out of the box, you may need to serialize more types or your own types.
In that case, you need to inherit
[ClassGeneratedValueSerializedTable](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Serialization/SerializedTables/ClassGeneratedValueSerializedTable.cs),
[ClassSerializedValueSerializedTable](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Serialization/SerializedTables/ClassSerializedValueSerializedTable.cs),
[StructGeneratedValueSerializedTable](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Serialization/SerializedTables/StructGeneratedValueSerializedTable.cs),
or [StructSerializedValueSerializedTable](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Serialization/SerializedTables/StructSerializedValueSerializedTable.cs).

If you need a full customizable serialized table, you can inherit
[SerializedTable_Base](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Serialization/SerializedTables/SerializedTable_Base.cs).

If you need a full customizable serialized container, you can inherit
[SerializedContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Serialization/SerializedContainer.cs).

## Blackboard editor support

Because Unity draws only serialized by itself properties, properties of a [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
aren't drawn by Unity. But [BlackboardContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardContainer.cs)
has a custom editor to show and make editable all of its [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) properties.
Although there are many types that are drawn out of the box, you may need to draw more types in Unity editor. You can achieve it by inheriting
[BlackboardValueView](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Editor/ValueViews/BlackboardValueView.cs) or
[UnityObjectBlackboardValueView](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Editor/ValueViews/Implementations/UnityObjectBlackboardValueView.cs).

## Logs

There is a special class for logging the whole Blackboard system:
[BlackboardDebug](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Debug/BlackboardDebug.cs).
It contains conditional methods for logging. You can control a compilation of those methods with define symbols:
- SIMPLE_BLACKBOARD_LOG_DETAILS - log every change of the Blackboard system.
- SIMPLE_BLACKBOARD_LOG
- SIMPLE_BLACKBOARD_LOG_WARNING
- SIMPLE_BLACKBOARD_LOG_ERROR

[BlackboardDebug](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Debug/BlackboardDebug.cs)
has all of them as public const strings.

## Multithreading

The package has an optional multithreading support. You can activate it with the define SIMPLE_BLACKBOARD_MULTITHREADING.
With that define, different locks are compiled. Those locks exist for internal usage only: access to 
[Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
in different classes of this package and internal work of 
[BlackboardPropertyName](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/BlackboardPropertyName.cs).
[Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
doesn't have internal locks. So, you have to save your code from issues yourself.

Also, you can use the package in a multithreading code without the define SIMPLE_BLACKBOARD_MULTITHREADING.
In that case, you have to guarantee that you don't access the same
[Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
from different threads and that you access the constructor and the name property of
[BlackboardPropertyName](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/BlackboardPropertyName.cs)
only from one thread.

## Little features

### [BlackboardPropertyReference](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardPropertyReference.cs)

It's a simple serializable struct with two fields: reference to
[BlackboardContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardContainer.cs)
and property name of string type. It has a custom editor that makes a selector for the property name field. The selector is filled with property names of
all serialized properties of the referenced
[BlackboardContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardContainer.cs).
It's recommended to use it for custom components where you need to reference a specific property in a
[Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
of a [BlackboardContainer](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Components/BlackboardContainer.cs).

### Accessors

These are special components that allow to easily get or set a value in a [Blackboard](https://github.com/ZorPastaman/Simple-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) in Unity component system.
You can find them in **Add Component/Simple Blackboard/Accessors/** or in the [folder](https://github.com/ZorPastaman/Simple-Blackboard/tree/develop/Runtime/Components/Accessors).
You can get/set a blackboard property via the property `Accessor<T, TEvent>.value`.

Accessors also have Unity Event that is invoked with a current value in a blackboard. To flush such an event, you need to call the method `Accessor_Base.Flush()` or just use one of Accessor flushers that do that automatically.
You can find them in **Add Component/Simple Blackboard/Accessor Flushers/** or in the [folder](https://github.com/ZorPastaman/Simple-Blackboard/tree/develop/Runtime/Components/AccessorFlushers).

## See also
- [Event Based Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard) - another version of a blackboard system which flush events when its properties are changed but doesn't support derivation.
