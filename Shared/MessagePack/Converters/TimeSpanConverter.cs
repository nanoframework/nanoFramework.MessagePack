// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics.CodeAnalysis;
using nanoFramework.MessagePack.Stream;

namespace nanoFramework.MessagePack.Converters
{
    internal class TimeSpanConverter : IConverter
    {
        private static void Write(TimeSpan value, IMessagePackWriter writer)
        {
            ConverterContext.GetConverter(typeof(long)).Write(value.Ticks, writer);
        }

        private static TimeSpan Read(IMessagePackReader reader)
        {
            var longValue = (long)ConverterContext.GetConverter(typeof(long)).Read(reader)!;

            return TimeSpan.FromTicks(longValue);
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((TimeSpan)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
