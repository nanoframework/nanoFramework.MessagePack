using System;
using nanoFramework.MessagePack.Stream;
using System.Diagnostics.CodeAnalysis;

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
