// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using nanoFramework.MessagePack.Dto;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;

namespace nanoFramework.MessagePack.Extensions
{
    internal static class NumberConverterHelper
    {
        internal static void WriteByteValue(this byte value, IMessagePackWriter writer)
        {
            writer.Write(value);
        }

        internal static void WriteUShortValue(ushort value, IMessagePackWriter writer)
        {
            unchecked
            {
                writer.Write((byte)((value >> 8) & 0xff));
                writer.Write((byte)(value & 0xff));
            }
        }

        internal static void WriteUIntValue(uint value, IMessagePackWriter writer)
        {
            unchecked
            {
                writer.Write((byte)((value >> 24) & 0xff));
                writer.Write((byte)((value >> 16) & 0xff));
                writer.Write((byte)((value >> 8) & 0xff));
                writer.Write((byte)(value & 0xff));
            }
        }

        internal static sbyte ReadInt8(IMessagePackReader reader)
        {
            var temp = reader.ReadByte();
            if (temp <= sbyte.MaxValue)
            {
                return (sbyte)temp;
            }

            return (sbyte)((int)temp - byte.MaxValue - 1);
        }

        internal static byte ReadUInt8(IMessagePackReader reader)
        {
            return reader.ReadByte();
        }

        internal static ushort ReadUInt16(IMessagePackReader reader)
        {
            return (ushort)((reader.ReadByte() << 8) + reader.ReadByte());
        }

        internal static short ReadInt16(IMessagePackReader reader)
        {
            var temp = ReadUInt16(reader);
            if (temp <= short.MaxValue)
            {
                return (short)temp;
            }

            return (short)((int)temp - 1 - ushort.MaxValue);
        }

        internal static int ReadInt32(IMessagePackReader reader)
        {
            var temp = ReadUInt32(reader);
            if (temp <= int.MaxValue)
            {
                return (int)temp;
            }

            return (int)((long)temp - 1 - uint.MaxValue);
        }

        internal static uint ReadUInt32(IMessagePackReader reader)
        {
            var temp = (uint)(reader.ReadByte() << 24);
            temp += (uint)reader.ReadByte() << 16;
            temp += (uint)reader.ReadByte() << 8;
            temp += reader.ReadByte();

            return temp;
        }

        internal static ulong ReadUInt64(IMessagePackReader reader)
        {
            var temp = (ulong)reader.ReadByte() << 56;
            temp += (ulong)reader.ReadByte() << 48;
            temp += (ulong)reader.ReadByte() << 40;
            temp += (ulong)reader.ReadByte() << 32;
            temp += (ulong)reader.ReadByte() << 24;
            temp += (ulong)reader.ReadByte() << 16;
            temp += (ulong)reader.ReadByte() << 8;
            temp += reader.ReadByte();

            return temp;
        }

        internal static long ReadInt64(this IMessagePackReader reader)
        {
            var temp = ReadUInt64(reader);
            if (temp <= long.MaxValue)
            {
                return (long)temp;
            }

            return (long)(temp - 1 - ulong.MaxValue);
        }

        private static bool TryWriteSignedFixNum(long value, IMessagePackWriter writer)
        {
            // positive fixnum
            if (value >= 0 && value < 128L)
            {
                writer.Write(unchecked((byte)value));
                return true;
            }

            // negative fixnum
            if (value >= -32L && value <= -1L)
            {
                writer.Write(unchecked((byte)value));
                return true;
            }

            return false;
        }

        private static bool TryWriteUnsignedFixNum(ulong value, IMessagePackWriter writer)
        {
            // positive fixnum
            if (value < 128L)
            {
                writer.Write(unchecked((byte)value));
                return true;
            }

            return false;
        }

        private static bool TryWriteInt8(long value, IMessagePackWriter writer)
        {
            if (value > sbyte.MaxValue || value < sbyte.MinValue)
            {
                return false;
            }

            writer.Write(DataTypes.Int8);
            WriteSByteValue((sbyte)value, writer);
            return true;
        }

        internal static bool TryGetFixPositiveNumber(DataTypes type, out byte temp)
        {
            temp = (byte)type;
            return type.GetHighBits(1) == DataTypes.PositiveFixNum.GetHighBits(1);
        }

        internal static bool TryGetNegativeNumber(DataTypes type, out sbyte temp)
        {
            temp = (sbyte)((byte)type - 1 - byte.MaxValue);

            return type.GetHighBits(3) == DataTypes.NegativeFixNum.GetHighBits(3);
        }

        internal static bool TryGetInt32(DataTypes type, IMessagePackReader reader, out int result)
        {
            result = int.MinValue;
            if (TryGetFixPositiveNumber(type, out byte temp))
            {
                result = temp;
                return true;
            }

            if (TryGetNegativeNumber(type, out sbyte tempInt8))
            {
                result = tempInt8;
                return true;
            }

            switch (type)
            {
                case DataTypes.UInt8:
                    result = ReadUInt8(reader);
                    return true;
                case DataTypes.UInt16:
                    result = ReadUInt16(reader);
                    return true;
                case DataTypes.UInt32:
                    var uintValue = ReadUInt32(reader);

                    if (uintValue <= int.MaxValue)
                    {
                        result = (int)uintValue;
                        return true;
                    }

                    return false;
                case DataTypes.Int8:
                    result = ReadInt8(reader);
                    return true;
                case DataTypes.Int16:
                    result = ReadInt16(reader);
                    return true;
                case DataTypes.Int32:
                    result = ReadInt32(reader);
                    return true;
                default:
                    return false;
            }
        }

        internal static bool TryGetInt64(DataTypes type, IMessagePackReader reader, out long result)
        {
            result = long.MaxValue;
            if (TryGetFixPositiveNumber(type, out byte tempUInt8))
            {
                result = tempUInt8;
                return true;
            }

            if (TryGetNegativeNumber(type, out sbyte tempInt8))
            {
                result = tempInt8;
                return true;
            }

            switch (type)
            {
                case DataTypes.UInt8:
                    result = ReadUInt8(reader);
                    return true;
                case DataTypes.UInt16:
                    result = ReadUInt16(reader);
                    return true;
                case DataTypes.UInt32:
                    result = ReadUInt32(reader);
                    return true;
                case DataTypes.UInt64:
                    var ulongValue = ReadUInt64(reader);
                    if (ulongValue <= long.MaxValue)
                    {
                        result = (long)ulongValue;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case DataTypes.Int8:
                    result = ReadInt8(reader);
                    return true;
                case DataTypes.Int16:
                    result = ReadInt16(reader);
                    return true;
                case DataTypes.Int32:
                    result = ReadInt32(reader);
                    return true;
                case DataTypes.Int64:
                    result = reader.ReadInt64();
                    return true;
                default:
                    return false;
            }
        }

        private static void WriteSByteValue(sbyte value, IMessagePackWriter writer)
        {
            writer.Write((byte)value);
        }

        private static bool TryWriteUInt8(ulong value, IMessagePackWriter writer)
        {
            if (value > byte.MaxValue)
            {
                return false;
            }

            writer.Write(DataTypes.UInt8);
            ((byte)value).WriteByteValue(writer);
            return true;
        }

        private static bool TryWriteInt16(long value, IMessagePackWriter writer)
        {
            if (value < short.MinValue || value > short.MaxValue)
            {
                return false;
            }

            writer.Write(DataTypes.Int16);
            WriteShortValue((short)value, writer);
            return true;
        }

        private static void WriteShortValue(short value, IMessagePackWriter writer)
        {
            unchecked
            {
                writer.Write((byte)((value >> 8) & 0xff));
                writer.Write((byte)(value & 0xff));
            }
        }

        private static bool TryWriteUInt16(ulong value, IMessagePackWriter writer)
        {
            if (value > ushort.MaxValue)
            {
                return false;
            }

            writer.Write(DataTypes.UInt16);
            WriteUShortValue((ushort)value, writer);
            return true;
        }

        private static bool TryWriteInt32(long value, IMessagePackWriter writer)
        {
            if (value > int.MaxValue || value < int.MinValue)
            {
                return false;
            }

            writer.Write(DataTypes.Int32);
            WriteIntValue((int)value, writer);
            return true;
        }

        private static void WriteIntValue(int value, IMessagePackWriter writer)
        {
            unchecked
            {
                writer.Write((byte)((value >> 24) & 0xff));
                writer.Write((byte)((value >> 16) & 0xff));
                writer.Write((byte)((value >> 8) & 0xff));
                writer.Write((byte)(value & 0xff));
            }
        }

        private static bool TryWriteUInt32(ulong value, IMessagePackWriter writer)
        {
            if (value > uint.MaxValue)
            {
                return false;
            }

            writer.Write(DataTypes.UInt32);
            WriteUIntValue((uint)value, writer);
            return true;
        }

        private static bool TryWriteInt64(long value, IMessagePackWriter writer)
        {
            writer.Write(DataTypes.Int64);
            value.WriteLongValue(writer);
            return true;
        }

        internal static void WriteLongValue(this long value, IMessagePackWriter writer)
        {
            unchecked
            {
                writer.Write((byte)((value >> 56) & 0xff));
                writer.Write((byte)((value >> 48) & 0xff));
                writer.Write((byte)((value >> 40) & 0xff));
                writer.Write((byte)((value >> 32) & 0xff));
                writer.Write((byte)((value >> 24) & 0xff));
                writer.Write((byte)((value >> 16) & 0xff));
                writer.Write((byte)((value >> 8) & 0xff));
                writer.Write((byte)(value & 0xff));
            }
        }

        private static bool TryWriteUInt64(ulong value, IMessagePackWriter writer)
        {
            writer.Write(DataTypes.UInt64);
            WriteULongValue(value, writer);
            return true;
        }

        private static void WriteULongValue(ulong value, IMessagePackWriter writer)
        {
            unchecked
            {
                writer.Write((byte)((value >> 56) & 0xff));
                writer.Write((byte)((value >> 48) & 0xff));
                writer.Write((byte)((value >> 40) & 0xff));
                writer.Write((byte)((value >> 32) & 0xff));
                writer.Write((byte)((value >> 24) & 0xff));
                writer.Write((byte)((value >> 16) & 0xff));
                writer.Write((byte)((value >> 8) & 0xff));
                writer.Write((byte)(value & 0xff));
            }
        }

        internal static void WriteInteger(long value, IMessagePackWriter writer)
        {
            if (value >= 0)
            {
                WriteNonNegativeInteger((ulong)value, writer);
                return;
            }

            if (TryWriteSignedFixNum(value, writer))
            {
                return;
            }

            if (TryWriteInt8(value, writer))
            {
                return;
            }

            if (TryWriteInt16(value, writer))
            {
                return;
            }

            if (TryWriteInt32(value, writer))
            {
                return;
            }

            if (TryWriteInt64(value, writer))
            {
                return;
            }

            throw ExceptionUtility.IntSerializationFailure(value);
        }

        internal static void WriteNonNegativeInteger(ulong value, IMessagePackWriter writer)
        {
            if (TryWriteUnsignedFixNum(value, writer))
            {
                return;
            }

            if (TryWriteUInt8(value, writer))
            {
                return;
            }

            if (TryWriteUInt16(value, writer))
            {
                return;
            }

            if (TryWriteUInt32(value, writer))
            {
                return;
            }

            if (TryWriteUInt64(value, writer))
            {
                return;
            }

            throw ExceptionUtility.IntSerializationFailure(value);
        }

        internal static float ReadFloat(IMessagePackReader reader)
        {
            var binary = ReadBytes(reader, 4);
            byte[] bytes;
            if (BitConverter.IsLittleEndian)
            {
                bytes = new[]
                {
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
                    binary[0],
                    binary[1],
                    binary[2],
                    binary[3]
                };
            }

            return BitConverter.ToSingle(bytes, 0);
        }

        internal static double ReadDouble(IMessagePackReader reader)
        {
            var binary = ReadBytes(reader, 8);
            byte[] bytes;
            if (BitConverter.IsLittleEndian)
            {
                bytes = new[]
                {
                    binary[7],
                    binary[6],
                    binary[5],
                    binary[4],
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
                    binary[0],
                    binary[1],
                    binary[2],
                    binary[3],
                    binary[4],
                    binary[5],
                    binary[6],
                    binary[7]
                };
            }

            return BitConverter.ToDouble(bytes, 0);
        }

        private static ArraySegment ReadBytes(IMessagePackReader reader, uint length)
        {
            return reader.ReadBytes(length);
        }
    }
}
