# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased - 2019-01-01
- [Commits](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/compare/0.0.0...0.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/milestone/0?closed=1)

### Added
- Nothing.

### Changed
- Nothing.

### Deprecated
- Nothing.

### Removed
- Nothing.

### Fixed
- Nothing.

### Security
- Nothing.

## 0.4.0-preview - 2019-12-07
- [Commits](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/compare/0.3.1-preview...0.4.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/milestone/5?closed=1)

### Added
- `ISerializeUtf8JsonTypeRegister` to register union types in serialize module.
- `SerializeUtf8JsonModuleDescription.TypeRegisters` to specify types used in serialization.

### Changed
- `SerializeUtf8JsonModuleDescription` to store `IJsonFormatterResolver` directly.
- `SerializeUtf8JsonModuleBuilderAsset` to work with reworked `SerializeUtf8JsonModuleDescription`.

## 0.3.1-preview - 2019-12-04
- [Commits](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/compare/0.3.0-preview...0.3.1-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/milestone/4?closed=1)

### Changed
- Package dependencies:
    - `com.ugf.utf8json`: from `3.0.0-preview` to `3.1.1-preview`.

### Fixed
- Generated Unity and Math resolver names.

## 0.3.0-preview - 2019-11-23
- [Commits](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/compare/0.2.0-preview...0.3.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/milestone/3?closed=1)

### Added
- `SerializeUtf8JsonModuleBuilderAsset`: resolvers list to initialize in module.

### Changed
- Package dependencies:
    - `com.ugf.module.serialize`: from `0.1.0-preview` to `0.2.0-preview`.
    - `com.ugf.utf8json`: from `2.2.0-preview` to `3.0.0-preview`.
- Change generated resolvers for math and Unity types.

## 0.2.0-preview - 2019-10-17
- [Commits](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/compare/0.1.0-preview...0.2.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/milestone/2?closed=1)

### Added
- `ISerializeUtf8JsonUnionProvider`: add `Remove` all types and `Clear` methods.
- `SerializeUtf8JsonUnionProviderExtensions`: with additional `Add`, `Remove` and `GetUnionType` methods.

### Changed
- Package dependencies:
    - `com.ugf.utf8json`: from `2.1.0-preview` to `2.2.0-preview`.

## 0.1.0-preview - 2019-10-09
- [Commits](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/compare/e4f3f22...0.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-serialize-utf8json/milestone/1?closed=1)

### Added
- This is a initial release.

---
> Unity Game Framework | Copyright 2019
