using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class BinaryConverter : IConverter
    {
        private readonly bool _compatibilityMode;

        public BinaryConverter(bool compatibilityMode = false)
        {
            _compatibilityMode = compatibilityMode;
        }

        private void Write(byte[] value, IMessagePackWriter writer)
        {
            if (value == null)
            {
                ConverterContext.NullConverter.Write(value, writer);
                return;
            }

            var valueLength = (uint)value.Length;
            if (_compatibilityMode)
            {
                WriteStringHeaderAndLength(valueLength, writer);
            }
            else
            {
                WriteBinaryHeaderAndLength(valueLength, writer);
            }

            writer.Write(value);
        }

        private byte[] Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            uint length;
            switch (type)
            {
                case DataTypes.Null:
                    return null!;

                case DataTypes.Bin8:
                    length = NumberConverterHelper.ReadUInt8(reader);
                    break;

                case DataTypes.Bin16:
                    length = NumberConverterHelper.ReadUInt16(reader);
                    break;

                case DataTypes.Bin32:
                    length = NumberConverterHelper.ReadUInt32(reader);
                    break;

                case DataTypes.Str8:
                    if (_compatibilityMode)
                        length = NumberConverterHelper.ReadUInt8(reader);
                    else
                        throw ExceptionUtility.CantReadStringAsBinary();
                    break;

                case DataTypes.Str16:
                    if (_compatibilityMode)
                        length = NumberConverterHelper.ReadUInt16(reader);
                    else
                        throw ExceptionUtility.CantReadStringAsBinary();
                    break;

                case DataTypes.Str32:
                    if (_compatibilityMode)
                        length = NumberConverterHelper.ReadUInt32(reader);
                    else
                        throw ExceptionUtility.CantReadStringAsBinary();
                    break;

                default:
                    if ((type & DataTypes.FixStr) == DataTypes.FixStr)
                    {
                        if (_compatibilityMode)
                            length = (uint)(type & ~DataTypes.FixStr);
                        else
                            throw ExceptionUtility.CantReadStringAsBinary();
                    }
                    else
                    {
                        throw ExceptionUtility.BadTypeException(type, DataTypes.Bin8, DataTypes.Bin16, DataTypes.Bin32, DataTypes.Null);
                    }
                    break;
            }

            var array = reader.ReadBytes(length);
            return (byte[]) array;
        }

        private static void WriteBinaryHeaderAndLength(uint length, IMessagePackWriter writer)
        {
            if (length <= byte.MaxValue)
            {
                writer.Write(DataTypes.Bin8);
                NumberConverterHelper.WriteByteValue((byte)length, writer);
            }
            else if (length <= ushort.MaxValue)
            {
                writer.Write(DataTypes.Bin16);
                NumberConverterHelper.WriteUShortValue((ushort)length, writer);
            }
            else
            {
                writer.Write(DataTypes.Bin32);
                NumberConverterHelper.WriteUIntValue(length, writer);
            }
        }

        private static void WriteStringHeaderAndLength(uint length, IMessagePackWriter writer)
        {
            if (length <= 31)
            {
                writer.Write((byte)(((byte)DataTypes.FixStr + length) % 256));
                return;
            }

            if (length <= ushort.MaxValue)
            {
                writer.Write(DataTypes.Str16);
                NumberConverterHelper.WriteUShortValue((ushort)length, writer);
            }
            else
            {
                writer.Write(DataTypes.Str32);
                NumberConverterHelper.WriteUIntValue((uint)length, writer);
            }
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((byte[])value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
