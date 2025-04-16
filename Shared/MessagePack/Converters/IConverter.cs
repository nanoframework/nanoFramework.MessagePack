// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using nanoFramework.MessagePack.Stream;

namespace nanoFramework.MessagePack.Converters
{
    /// <summary>
    /// <see cref="Converter"/> interface.
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Write object in to target MessagePack stream.
        /// </summary>
        /// <param name="value">Source object.</param>
        /// <param name="writer">Target MessagePack stream.</param>
#nullable enable
        void Write(object? value, [NotNull] IMessagePackWriter writer);

        /// <summary>
        /// Read object from MessagePack stream.
        /// </summary>
        /// <param name="reader">MessagePack stream.</param>
        /// <returns>Readied object.</returns>
        object? Read([NotNull] IMessagePackReader reader);
    }
}
