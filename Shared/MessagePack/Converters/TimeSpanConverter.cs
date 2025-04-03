
using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using System;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class TimeSpanConverter : IConverter
    {
        public void Write(TimeSpan value, IMessagePackWriter writer)
        {
            ConverterContext.GetConverter(typeof(long)).Write(value.Ticks, writer);
        }

        public TimeSpan Read(IMessagePackReader reader)
        {
            var longValue = (long)ConverterContext.GetConverter(typeof(long)).Read(reader)!;

            return TimeSpan.FromTicks(longValue);
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((TimeSpan)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
