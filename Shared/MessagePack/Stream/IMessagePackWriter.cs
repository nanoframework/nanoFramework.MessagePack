// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.MessagePack.Stream
{
    /// <summary>
    /// MessagePack writer interface
    /// </summary>
    public interface IMessagePackWriter
    {
        /// <summary>
        /// Write MessagePack data type
        /// </summary>
        /// <param name="dataType"></param>
        void Write(DataTypes dataType);

        /// <summary>
        /// Write byte value
        /// </summary>
        /// <param name="value"></param>
        void Write(byte value);

        /// <summary>
        /// Write byte array
        /// </summary>
        /// <param name="array">Writhed bytes</param>
        void Write(byte[] array);

        /// <summary>
        /// Write array MessagePack header
        /// </summary>
        /// <param name="length">Array length</param>
        void WriteArrayHeader(uint length);

        /// <summary>
        /// Write MessagePack map header
        /// </summary>
        /// <param name="length">Map items length</param>
        void WriteMapHeader(uint length);
    }
}
