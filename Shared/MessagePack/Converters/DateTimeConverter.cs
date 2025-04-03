using nanoFramework.MessagePack.Stream;
using System;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class DateTimeConverter : IConverter
    {
        object IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }

        public void Write(DateTime value, IMessagePackWriter writer)
        {
            var longValue = value.Subtract(DateTime.UnixEpoch).Ticks;

            ConverterContext.GetConverter(typeof(long)).Write(longValue, writer);
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((DateTime)value, writer);
        }

        public DateTime Read(IMessagePackReader reader)
        {
            var longValue = (long)ConverterContext.GetConverter(typeof(long)).Read(reader)!;
            return DateTime.UnixEpoch.AddTicks(longValue);
        }
    }
}
