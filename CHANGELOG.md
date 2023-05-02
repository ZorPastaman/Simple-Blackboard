# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed

- BlackboardPropertyName now uses FNV-1a hash algorithm.

### Fixed

- Exception while setting a value of type of Unity Object into a blackboard in a non-main thread 
when SIMPLE_BLACKBOARD_LOG_DETAILS define is enabled. 

## [1.4.0] - 2021-03-12

### Added

- Package icon.

### Fixed

- Add popup close button icon is now visible.

### Changed

- BlackboardContainer is renamed to SimpleBlackboardContainer.
- SerializedContainer is renamed to SimpleSerializedContainer.
- SerializedTablesContainer is renamed to SimpleSerializedTablesContainer.
- Blackboard parts of editor ui are rewritten with ui elements.
- Unity 2019.4 is now required.

## [1.3.0] - 2021-01-23

### Added

- Multithreading support: different locks are added. The feature is optional and controlled 
   by the define SIMPLE_BLACKBOARD_MULTITHREADING.
- Blackboard now inherits ICollection interfaces.

### Changed

- Initial capacity of blackboard property names is changed from 100 to 1000.

## [1.2.0] - 2020-11-21

### Added

- Serialization of local components in Blackboard Container.
- Blackboard.GetValueType() method that returns a type of a property by its property name.
- Blackboard.GetPropertyNames() method that returns all property names of properties contained in Blackboard.
- Blackboard.CopyTo() methods that copy properties to another blackboard.

## [1.1.0] - 2020-09-28

### Changed

- Some methods became aggressively inlined for performance reasons.
- Some methods got a pure attribute.
- Components and Scriptable objects became editable from code.

## [1.0.0] - 2020-07-15

### Added

- Blackboard and other core functionality.
- Blackboard serialization.
- Unity components support.
- Unity editor support.
- Tests.

[unreleased]: https://github.com/ZorPastaman/Simple-Blackboard/compare/v1.4.0...HEAD
[1.4.0]: https://github.com/ZorPastaman/Simple-Blackboard/releases/tag/v1.4.0
[1.3.0]: https://github.com/ZorPastaman/Simple-Blackboard/releases/tag/v1.3.0
[1.2.0]: https://github.com/ZorPastaman/Simple-Blackboard/releases/tag/v1.2.0
[1.1.0]: https://github.com/ZorPastaman/Simple-Blackboard/releases/tag/v1.1.0
[1.0.0]: https://github.com/ZorPastaman/Simple-Blackboard/releases/tag/v1.0.0
