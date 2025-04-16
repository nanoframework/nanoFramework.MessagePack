// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics.CodeAnalysis;
using nanoFramework.MessagePack.Stream;

namespace nanoFramework.MessagePack.Converters
{
    internal class CharConverter : IConverter
    {
        private static void Write(char value, IMessagePackWriter writer)
        {
            ConverterContext.GetConverter(typeof(ushort)).Write(BitConverter.ToUInt16(BitConverter.GetBytes(value), 0), writer);
        }

        private static char Read(IMessagePackReader reader)
        {
            return BitConverter.ToChar(BitConverter.GetBytes((ushort)ConverterContext.GetConverter(typeof(ushort)).Read(reader)!), 0);
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((char)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
