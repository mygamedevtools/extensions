# [2.0.0](https://github.com/mygamedevtools/extensions/compare/1.4.0...2.0.0) (2023-03-24)


### Code Refactoring

* update namespaces and assemblies ([20a4d51](https://github.com/mygamedevtools/extensions/commit/20a4d518af09d473851f891eec70a1a73282ab0f))


### Features

* add extensions to find interface implements ([a61e665](https://github.com/mygamedevtools/extensions/commit/a61e665e83365c2c43e896f6edccfbfcf76a1154))


### BREAKING CHANGES

* additional assemblies and namespaces created

Both GroupGameObjects and ScriptingDefineSymbolsHelper classes were moved to the MyGameDevTools.Tooling namespace and assembly. All the attributes have been moved to the MyGameDevTools.Attributes namespace and assembly.

# [1.4.0](https://github.com/mygamedevtools/extensions/compare/1.3.1...1.4.0) (2022-10-25)


### Bug Fixes

* Add missing persist-credentials to workflow ([cd288e6](https://github.com/mygamedevtools/extensions/commit/cd288e6edaa8ff57c1eb6dbe8c59ed5083669d7e))


### Features

* Add CI configuration ([27eb0fc](https://github.com/mygamedevtools/extensions/commit/27eb0fcbff3c8ee864757f1e54c95014b18f6505))

## [1.3.1] - 2022-10-24
- Added: OpenUPM documentation.

## [1.3.0] - 2022-10-03
- Changed: Updated organization name to comply with [Unity Package Guidelines](https://unity.com/legal/terms-of-service/software/package-guidelines).
- Fixed: Updated assembly definition and namespaces names to reflect the organization name changes.

## [1.2.0] - 2022-09-13
- Changed: Moved repository to My Unity Tools organization
- Changed: Updated package name and author 

## [1.1.0] - 2022-07-06
- Changed: Updated package name to `My Unity Tools - Extensions`.
- Changed: Updated asmdef files to add the `Extensions` namespace: `mygamedevtools` -> `mygamedevtools.Extensions`.
- Added: `ScriptingDefineSymbolsHelper` to help managing scripting define symbols.

## [1.0.0] - 2022-02-22
- Added: `LabeledArrayAttribute` and `LabeledArrayDrawer` to allow labeling array elements.
- Added: `ReadOnlyAttribute` and `ReadOnlyDrawer` to prevent editing the field in the inspector.
- Added: `SceneFieldAttribute` and `SceneFieldDrawer` to allow dragging scene references to fields in the inspector.
- Added: `DebugExtensions` to help debugging arrays and collections.
- Added: `GameObjectExtensions` to help with common `GameObject` operations.

[1.3.1]: https://github.com/mygamedevtools/extensions/compare/1.3.0...1.3.1
[1.3.0]: https://github.com/mygamedevtools/extensions/compare/1.2.0...1.3.0
[1.2.0]: https://github.com/mygamedevtools/extensions/compare/1.1.0...1.2.0
[1.1.0]: https://github.com/mygamedevtools/extensions/compare/1.0.0...1.1.0
[1.0.0]: https://github.com/mygamedevtools/extensions/compare/593b818...1.0.0
