// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.MessagePack
{
    /// <summary>
    /// Supporting MessagePack types.
    /// </summary>
    public enum DataTypes : byte
    {
        /// <summary>
        /// Represents a null value in MessagePack.
        /// </summary>
        Null = 0xc0,

        /// <summary>
        /// Represents a boolean false value in MessagePack.
        /// </summary>
        False = 0xc2,

        /// <summary>
        /// Represents a boolean true value in MessagePack.
        /// </summary>
        True = 0xc3,

        /// <summary>
        /// Represents a 32-bit floating-point number in MessagePack.
        /// </summary>
        Single = 0xca,

        /// <summary>
        /// Represents a 64-bit floating-point number in MessagePack.
        /// </summary>
        Double = 0xcb,

        /// <summary>
        /// Represents an unsigned 8-bit integer in MessagePack.
        /// </summary>
        UInt8 = 0xcc,

        /// <summary>
        /// Represents an unsigned 16-bit integer in MessagePack.
        /// </summary>
        UInt16 = 0xcd,

        /// <summary>
        /// Represents an unsigned 32-bit integer in MessagePack.
        /// </summary>
        UInt32 = 0xce,

        /// <summary>
        /// Represents an unsigned 64-bit integer in MessagePack.
        /// </summary>
        UInt64 = 0xcf,

        /// <summary>
        /// Represents a negative fixed integer in MessagePack (last 5 bits are the value).
        /// </summary>
        /// <remarks>Last 5 bits is the value.</remarks>
        NegativeFixNum = 0xe0,

        /// <summary>
        /// Represents a positive fixed integer in MessagePack.
        /// </summary>
        PositiveFixNum = 0x7f,

        /// <summary>
        /// Represents a signed 8-bit integer in MessagePack.
        /// </summary>
        Int8 = 0xd0,

        /// <summary>
        /// Represents a signed 16-bit integer in MessagePack.
        /// </summary>
        Int16 = 0xd1,

        /// <summary>
        /// Represents a signed 32-bit integer in MessagePack.
        /// </summary>
        Int32 = 0xd2,

        /// <summary>
        /// Represents a signed 64-bit integer in MessagePack.
        /// </summary>
        Int64 = 0xd3,

        /// <summary>
        /// Represents a fixed-size array in MessagePack (last 4 bits are the size).
        /// </summary>
        /// <remarks>Last 4 bits is the size.</remarks>
        FixArray = 0x90,

        /// <summary>
        /// Represents an array with up to 16-bit length in MessagePack.
        /// </summary>
        Array16 = 0xdc,

        /// <summary>
        /// Represents an array with up to 32-bit length in MessagePack.
        /// </summary>
        Array32 = 0xdd,

        /// <summary>
        /// Represents a fixed-size map in MessagePack (last 4 bits are the size).
        /// </summary>
        /// <remarks>Last 4 bits is the size.</remarks>
        FixMap = 0x80,

        /// <summary>
        /// Represents a map with up to 16-bit length in MessagePack.
        /// </summary>
        Map16 = 0xde,

        /// <summary>
        /// Represents a map with up to 32-bit length in MessagePack.
        /// </summary>
        Map32 = 0xdf,

        /// <summary>
        /// Represents a fixed-size string in MessagePack (last 5 bits are the size).
        /// </summary>
        /// <remarks>Last 5 bits is the length.</remarks>
        FixStr = 0xa0,

        /// <summary>
        /// Represents a string with up to 8-bit length in MessagePack.
        /// </summary>
        Str8 = 0xd9,

        /// <summary>
        /// Represents a string with up to 16-bit length in MessagePack.
        /// </summary>
        Str16 = 0xda,

        /// <summary>
        /// Represents a string with up to 32-bit length in MessagePack.
        /// </summary>
        Str32 = 0xdb,

        /// <summary>
        /// Represents a binary data with up to 8-bit length in MessagePack.
        /// </summary>
        Bin8 = 0xc4,

        /// <summary>
        /// Represents a binary data with up to 16-bit length in MessagePack.
        /// </summary>
        Bin16 = 0xc5,

        /// <summary>
        /// Represents a binary data with up to 32-bit length in MessagePack.
        /// </summary>
        Bin32 = 0xc6,

        /// <summary>
        /// Represents a 32-bit timestamp in MessagePack.
        /// </summary>
        Timestamp32 = 0xd6,

        /// <summary>
        /// Represents a 64-bit timestamp in MessagePack.
        /// </summary>
        Timestamp64 = 0xd7,

        /// <summary>
        /// Represents a 96-bit timestamp in MessagePack.
        /// </summary>
        Timestamp96 = 0xc7
    }
}
