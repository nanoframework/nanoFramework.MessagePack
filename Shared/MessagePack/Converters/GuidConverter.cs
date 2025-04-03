using nanoFramework.MessagePack.Stream;
using System;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class GuidConverter : IConverter
    {
        public Guid Read([NotNull] IMessagePackReader reader)
        {
            return new Guid((byte[])ConverterContext.GetConverter(typeof(byte[])).Read(reader)!);
        }

        public void Write(Guid value, [NotNull] IMessagePackWriter writer)
        {
            ConverterContext.GetConverter(typeof(byte[])).Write(value.ToByteArray(), writer);
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((Guid)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
