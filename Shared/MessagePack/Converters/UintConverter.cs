using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class UintConverter : IConverter
    {
        public void Write(uint value, IMessagePackWriter writer)
        {
            NumberConverterHelper.WriteNonNegativeInteger(value, writer);
        }

        public uint Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetFixPositiveNumber(type, out byte temp))
            {
                return temp;
            }

            if (NumberConverterHelper.TryGetNegativeNumber(type, out sbyte tempInt8))
            {
                return (uint)tempInt8;
            }

            return type switch
            {
                DataTypes.UInt8 => NumberConverterHelper.ReadUInt8(reader),
                DataTypes.UInt16 => NumberConverterHelper.ReadUInt16(reader),
                DataTypes.UInt32 => NumberConverterHelper.ReadUInt32(reader),
                DataTypes.Int8 => (uint)NumberConverterHelper.ReadInt8(reader),
                DataTypes.Int16 => (uint)NumberConverterHelper.ReadInt16(reader),
                DataTypes.Int32 => (uint)NumberConverterHelper.ReadInt32(reader),
                _ => throw ExceptionUtility.IntDeserializationFailure(type),
            };
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((uint)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
