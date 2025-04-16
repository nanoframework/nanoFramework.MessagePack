// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics.CodeAnalysis;
using nanoFramework.MessagePack.Stream;

namespace nanoFramework.MessagePack.Converters
{
    internal class DateTimeConverter : IConverter
    {
        private static DateTime Read(IMessagePackReader reader)
        {
            var longValue = (long)ConverterContext.GetConverter(typeof(long)).Read(reader)!;
            return DateTime.UnixEpoch.AddTicks(longValue);
        }


        private static void Write(DateTime value, IMessagePackWriter writer)
        {
            var longValue = value.Subtract(DateTime.UnixEpoch).Ticks;

            ConverterContext.GetConverter(typeof(long)).Write(longValue, writer);
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((DateTime)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
