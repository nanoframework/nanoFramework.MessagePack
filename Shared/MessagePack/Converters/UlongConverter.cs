using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class UlongConverter : IConverter
    {
        public void Write(ulong value, IMessagePackWriter writer)
        {
            NumberConverterHelper.WriteNonNegativeInteger(value, writer);
        }

        public ulong Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetFixPositiveNumber(type, out byte temp))
            {
                return temp;
            }

            if (NumberConverterHelper.TryGetNegativeNumber(type, out sbyte tempInt8))
            {
                return (ulong)tempInt8;
            }

            return type switch
            {
                DataTypes.UInt8 => NumberConverterHelper.ReadUInt8(reader),
                DataTypes.UInt16 => NumberConverterHelper.ReadUInt16(reader),
                DataTypes.UInt32 => NumberConverterHelper.ReadUInt32(reader),
                DataTypes.UInt64 => NumberConverterHelper.ReadUInt64(reader),
                DataTypes.Int8 => (ulong)NumberConverterHelper.ReadInt8(reader),
                DataTypes.Int16 => (ulong)NumberConverterHelper.ReadInt16(reader),
                DataTypes.Int32 => (ulong)NumberConverterHelper.ReadInt32(reader),
                DataTypes.Int64 => (ulong)reader.ReadInt64(),
                _ => throw ExceptionUtility.IntDeserializationFailure(type),
            };
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((ulong)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
