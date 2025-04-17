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

If you want to work with your own types, first thing you need to add is a type converter. Although the library provides almost complete coverage of the serialization/deserialization of any objects, there are certain cases where it is difficult to do without a custom converter.

#### The general principle of implementing a custom converter

1) In the code of your project, you need to create a converter class that inherits the IConverter interface and implement the Read and Write interface methods:

   ```csharp
        public class SimpleCustomConverter : IConverter
        {
            #nullable enable
            public void Write(object? value, [NotNull] IMessagePackWriter writer)
            {
                //TODO Your code is here
            }
    
            public object? Read([NotNull] IMessagePackReader reader)
            {
               var yourObject = new YourObject();
               //TODO Your code is here
               return yourObject;
            }
        }
   ```
   
2) Register your custom converter in the context of serialization:

   ```csharp
         public class Program
         {
             public static void Main()
             {
                   var simpleCustomConverter = new SimpleCustomConverter();
                   ConverterContext.Add(typeof(YourObject), simpleCustomConverter);
             }
         }
   ```
   
After completing these steps, the serialization/deserialization of the object for which your converter is added will occur in the methods of the custom converter.

##### A few examples of the implementation of custom converters

1. Case with enumeration elements as strings:
   
    ```csharp
        namespace samples
        {
            public enum FieldType
            {
                _ = -1,
                Str,
                Num,
                Any
            }
    
            public class FieldTypeConverter : IConverter
            {
                internal FieldType Read(IMessagePackReader reader)
                {
                    var stringConverter = ConverterContext.GetConverter(typeof(string));
        
                    var enumString = (string)stringConverter.Read(reader);
        
                    return enumString switch
                    {
                        "Str" => FieldType.Str,
                        "Num" => FieldType.Num,
                        "*" => FieldType.Any,
                        _ => throw new Exception($"Unexpected enum {typeof(FieldType)} underlying type: {enumString}"),
                    };
                }
        
                internal void Write(FieldType value, [NotNull] IMessagePackWriter writer)
                {
                    var stringConverter = ConverterContext.GetConverter(typeof(string));
        
                    switch (value)
                    {
                        case FieldType.Str:
                            stringConverter.Write("Str", writer);
                            break;
                        case FieldType.Num:
                            stringConverter.Write("Num", writer);
                            break;
                        case FieldType.Any:
                            stringConverter.Write("*", writer);
                            break;
                        default:
                            throw new Exception($"Enum {value.GetType()} value: {value} expected");
                    }
                }
        
        #nullable enable
                object? IConverter.Read([NotNull] IMessagePackReader reader)
                {
                    return Read(reader);
                }
        
                public void Write(object? value, [NotNull] IMessagePackWriter writer)
                {
                    Write((FieldType)value!, writer);
                }
            }
        }
   ```

2. Case compression or concealment of transmitted strings between sender and recipient if sender and recipient share the same vocabulary of words:

    ```csharp
        namespace samples
        {
            public static class SharedWordDictionary
            {
                static SharedWordDictionary()
                {
                    WordDictionary = new ArrayList
                    {
                        "MessagePak",
                        "Hello",
                        "at",
                        "nanoFramework!",
                        " "
                    };
                }
        
                public static ArrayList WordDictionary { get; }
            }
        
            public class SecureMessage
            {
                public SecureMessage(string message)
                {
                    Message = message;
                }
        
                public string Message { get; private set; }
            }
        
            public class SecureMessageConverter : IConverter
            {
                public SecureMessage Read([NotNull] IMessagePackReader reader)
                {
                    StringBuilder sb = new();
                    var length = reader.ReadArrayLength();
        
                    for(int i = 0; i < length; i++)
                    {
                        sb.Append(SharedWordDictionary.WordDictionary[i]);
                        sb.Append(' ');
                    }
                    if (sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
        
                    return new SecureMessage(sb.ToString());
                }
        
                public void Write(SecureMessage value, [NotNull] IMessagePackWriter writer)
                {
                    var messageWords = value.Message.Split(' ');
        
                    uint length = BitConverter.ToUInt32(BitConverter.GetBytes(messageWords.Length), 0);
                    writer.WriteArrayHeader(length);
        
                    var intConverter = ConverterContext.GetConverter(typeof(int));
        
                    foreach (var word in messageWords)
                    {
                        intConverter.Write(SharedWordDictionary.WordDictionary.IndexOf(word), writer);
                    }
                }
        
        #nullable enable
                object? IConverter.Read([NotNull] IMessagePackReader reader)
                {
                    return Read(reader);
                }
        
                public void Write(object? value, [NotNull] IMessagePackWriter writer)
                {
                    Write((SecureMessage)value!, writer);
                }
            }
        
            public class Program
            {
                public static void Main()
                {
                    var secureMessageConverter = new SecureMessageConverter();
                    ConverterContext.Add(typeof(SecureMessage), secureMessageConverter);
        
                    var secureMessage = new SecureMessage("Hello MessagePack at nanoFramework!");
        
                    //At sender
                    var buffer = MessagePackSerializer.Serialize(secureMessage);
                    Debug.WriteLine($"The message:\n{secureMessage.Message}\nbeing sent has been serialized into {buffer.Length} bytes.");
                    //and sent to recipient
                    //
                    //..........................
                    Debug.WriteLine("=============================================");
        
                    //At recipient, after receiving the byte array
                    Debug.WriteLine($"Received {buffer.Length} bytes");
        
                    var recipientSecureMessage = (SecureMessage)MessagePackSerializer.Deserialize(typeof(SecureMessage), buffer)!;
        
                    Debug.WriteLine($"Message received:\n{recipientSecureMessage.Message}");
                }
            }
        }
   ```

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
