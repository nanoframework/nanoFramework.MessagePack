using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class SbyteConverter: IConverter
    {
        private static void Write(sbyte value, IMessagePackWriter writer)
        {
            NumberConverterHelper.WriteInteger(value, writer);
        }

        private static sbyte Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetFixPositiveNumber(type, out byte temp))
            {
                return (sbyte)temp;
            }

            if (NumberConverterHelper.TryGetNegativeNumber(type, out sbyte tempInt8))
            {
                return tempInt8;
            }

            if (type == DataTypes.Int8)
            {
                return NumberConverterHelper.ReadInt8(reader);
            }

            throw ExceptionUtility.IntDeserializationFailure(type);
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((sbyte)value!, writer);
        }

        object IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
