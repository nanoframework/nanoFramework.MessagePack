[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_nanoFramework.MessagePack&metric=alert_status)](https://sonarcloud.io/dashboard?id=nanoframework_nanoFramework.MessagePack) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_nanoFramework.MessagePack&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=nanoframework_nanoFramework.MessagePack) [![NuGet](https://img.shields.io/nuget/dt/nanoFramework.MessagePack.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.MessagePack/) [![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md) [![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)

![nanoFramework logo](https://raw.githubusercontent.com/nanoframework/Home/main/resources/logo/nanoFramework-repo-logo.png)

-----

# Welcome to the .NET **nanoFramework** MessagePack repository

This repository contains the MessagePack library for the .NET **nanoFramework**. It provides high-performance serialization and deserialization with the smallest possible payload, [MessagePack](https://github.com/msgpack/msgpack) is an object serialization specification like JSON.

## Build status

| Component | Build Status | NuGet Package |
|:-|---|---|
| nanoFramework.MessagePack | [![Build Status](https://dev.azure.com/nanoframework/nanoFramework.MessagePack/_apis/build/status%2Fnanoframework.nanoFramework.MessagePack?branchName=main)](https://dev.azure.com/nanoframework/nanoFramework.MessagePack/_build/latest?definitionId=119&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.MessagePack.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.MessagePack/) |

# nanoFramework.MessagePack

MessagePack is a simple, lightweight serialization library, inspired by [MsgPack.Light](https://github.com/progaudi/MsgPack.Light), that can be used in .NET[nanoFramework](https://github.com/nanoframework) solutions.

## Usage

### Serialization to byte array

```csharp
var value = new TestClass();
var bytes = MessagePackSerializer.Serialize(value);
```

### Deserialization

```csharp
var result = (TestClass)MessagePackSerializer.Deserialize(typeof(TestClass), bytes);
```

### Your type serialization/deserialization

If you want to work with your own types, first thing you need to add is a type converter.

## Acknowledgements

The initial version of the MessagePack library was coded by [Spirin Dmitriy](https://github.com/RelaxSpirit), who has kindly handed over the library to the .NET **nanoFramework** project.

## Feedback and documentation

For documentation, providing feedback, issues, and finding out how to contribute, please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The **nanoFramework** WebServer library is licensed under the [MIT license](LICENSE.md).

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behaviour in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

## .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
