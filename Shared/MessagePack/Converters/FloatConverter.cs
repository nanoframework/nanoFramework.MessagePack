using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class FloatConverter : IConverter
    {
        public void Write(float value, IMessagePackWriter writer)
        {
            var binary = BitConverter.GetBytes(value);
            byte[] bytes;
            if (BitConverter.IsLittleEndian)
            {
                bytes = new[]
                {
                    (byte) DataTypes.Single,
                    binary[3],
                    binary[2],
                    binary[1],
                    binary[0]
                };
            }
            else
            {
                bytes = new[]
                {
                    (byte) DataTypes.Single,
                    binary[0],
                    binary[1],
                    binary[2],
                    binary[3]
                };
            }

            writer.Write(bytes);
        }

        public float Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (type == DataTypes.Single)
            {
                return NumberConverterHelper.ReadFloat(reader);
            }

            if (NumberConverterHelper.TryGetInt32(type, reader, out var result))
                return result;
            else
                throw ExceptionUtility.BadTypeException(type, DataTypes.Single);
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((float)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
