using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class ShortConverter : IConverter
    {
        private static void Write(short value, IMessagePackWriter writer)
        {
            NumberConverterHelper.WriteInteger(value, writer);
        }

        private static short Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetFixPositiveNumber(type, out byte temp))
            {
                return temp;
            }

            if (NumberConverterHelper.TryGetNegativeNumber(type, out sbyte tempInt8))
            {
                return tempInt8;
            }

            switch (type)
            {
                case DataTypes.UInt8:
                    return NumberConverterHelper.ReadUInt8(reader);

                case DataTypes.UInt16:
                    var ushortValue = NumberConverterHelper.ReadUInt16(reader);
                    if (ushortValue <= short.MaxValue)
                    {
                        return (short)ushortValue;
                    }

                    throw ExceptionUtility.IntDeserializationFailure(type);

                case DataTypes.Int8:
                    return NumberConverterHelper.ReadInt8(reader);

                case DataTypes.Int16:
                    return NumberConverterHelper.ReadInt16(reader);

                default:
                    throw ExceptionUtility.IntDeserializationFailure(type);
            }
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((short)value!, writer);
        }

        object IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
