using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class ByteConverter: IConverter
    {
        private static void Write(byte value, IMessagePackWriter writer)
        {
            // positive fixnum
            if (value < 128L)
            {
                writer.Write(value);
            }
            else
            {
                writer.Write(DataTypes.UInt8);
                value.WriteByteValue(writer);
            }
        }

        private static byte Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetFixPositiveNumber(type, out byte temp))
            {
                return temp;
            }

            if (NumberConverterHelper.TryGetNegativeNumber(type, out sbyte tempInt8))
            {
                return (byte)tempInt8;
            }

            return type switch
            {
                DataTypes.UInt8 => NumberConverterHelper.ReadUInt8(reader),
                DataTypes.Int8 => (byte)NumberConverterHelper.ReadInt8(reader),
                _ => throw ExceptionUtility.IntDeserializationFailure(type),
            };
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((byte)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
