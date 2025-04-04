﻿using nanoFramework.MessagePack.Stream;
using System;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class GuidConverter : IConverter
    {
        private static Guid Read([NotNull] IMessagePackReader reader)
        {
            return new Guid((byte[])ConverterContext.GetConverter(typeof(byte[])).Read(reader)!);
        }

        private static void Write(Guid value, [NotNull] IMessagePackWriter writer)
        {
            ConverterContext.GetConverter(typeof(byte[])).Write(value.ToByteArray(), writer);
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((Guid)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
