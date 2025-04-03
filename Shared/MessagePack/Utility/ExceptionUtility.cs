using System.Reflection;
using System;
using nanoFramework.MessagePack.Exceptions;
using nanoFramework.MessagePack.Extensions;

namespace nanoFramework.MessagePack.Utility
{
    internal static class ExceptionUtility
    {
        public static Exception BadTypeException(DataTypes actual, params DataTypes[] expectedCodes)
        {
            return new SerializationException($"Got {actual:G} (0x{actual:X}), while expecting one of these: {expectedCodes.JoinToString(", ")}");
        }

        public static Exception CantReadStringAsBinary()
        {
            return new SerializationException($"Reading a string as a byte array is disabled. Set 'binaryCompatibilityMode' parameter in MsgPackContext constructor to true to enable it");
        }

        public static Exception NotEnoughBytes(int actual, int expected)
        {
            return new SerializationException($"Expected {expected} bytes, got {actual} bytes.");
        }

        public static Exception NotEnoughBytes(long actual, long expected)
        {
            return new SerializationException($"Expected {expected} bytes, got {actual} bytes.");
        }

        public static Exception NoConverterForCollectionElement(Type type, string elementName)
        {
            return new SerializationException($"Provide converter for {elementName}: {type.Name}");
        }

        public static Exception IntDeserializationFailure(DataTypes type)
        {
            return new SerializationException($"Waited for an int, got {type:G} (0x{type:X})");
        }

        public static Exception IntSerializationFailure(long value)
        {
            return new SerializationException($"Can't serialize {value}");
        }

        public static Exception IntSerializationFailure(ulong value)
        {
            return new SerializationException($"Can't serialize {value}");
        }

        public static Exception ConverterNotFound(Type type)
        {
            return new ConverterNotFoundException(type);
        }
    }
}
