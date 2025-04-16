// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IO;
using nanoFramework.MessagePack.Dto;

namespace nanoFramework.MessagePack.Stream
{
    /// <summary>
    /// Read MessagePack interface
    /// </summary>
    public interface IMessagePackReader
    {
        /// <summary>
        /// Read MessagePack data type
        /// </summary>
        /// <returns>MessagePack item type <see cref="DataTypes"/></returns>
        DataTypes ReadDataType();

        /// <summary>
        /// Read one byte in MessagePack stream
        /// </summary>
        /// <returns></returns>
        byte ReadByte();

        /// <summary>
        /// Read bytes in MessagePack stream
        /// </summary>
        /// <param name="length">Length bytes for reading</param>
        /// <returns>Array reading <see cref="ArraySegment"/></returns>
        ArraySegment ReadBytes(uint length);

        /// <summary>
        /// Seek bytes in MessagePack stream
        /// </summary>
        /// <param name="offset">Offset from stream</param>
        /// <param name="origin">The position in the flow to offset</param>
        void Seek(long offset, SeekOrigin origin);

        /// <summary>
        /// Read MessagePack array length
        /// </summary>
        /// <returns>Array length</returns>
        uint ReadArrayLength();

        /// <summary>
        /// Read MessagePack map length
        /// </summary>
        /// <returns>Map items length</returns>
        uint ReadMapLength();

        /// <summary>
        /// Skip MessagePack item
        /// </summary>
        void SkipToken();

        /// <summary>
        /// Read MessagePack item
        /// </summary>
        /// <returns>Bytes MessagePack item <see cref="ArraySegment"/></returns>
#nullable enable
        ArraySegment? ReadToken();
    }
}
