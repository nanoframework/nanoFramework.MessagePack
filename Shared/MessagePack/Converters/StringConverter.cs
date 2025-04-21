﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using nanoFramework.MessagePack.Dto;
using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;

namespace nanoFramework.MessagePack.Converters
{
    internal class StringConverter : IConverter
    {
        private static readonly Encoding Utf8 = new UTF8Encoding();

        private static void Write(string value, IMessagePackWriter writer)
        {
            if (value == null)
            {
                ConverterContext.NullConverter.Write(value, writer);
            }
            else
            {
                var data = Utf8.GetBytes(value);

                WriteStringHeaderAndLength(writer, data.Length);

                writer.Write(data);
            }
        }

        private static string Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            switch (type)
            {
                case DataTypes.Null:
                    return null!;

                case DataTypes.Str8:
                    return ReadString(reader, NumberConverterHelper.ReadUInt8(reader));

                case DataTypes.Str16:
                    return ReadString(reader, NumberConverterHelper.ReadUInt16(reader));

                case DataTypes.Str32:
                    return ReadString(reader, NumberConverterHelper.ReadUInt32(reader));
            }

            if (TryGetFixStrLength(type, out var length))
            {
                return ReadString(reader, length);
            }

            throw ExceptionUtility.BadTypeException(type, DataTypes.FixStr, DataTypes.Str8, DataTypes.Str16, DataTypes.Str32);
        }

        internal static string ReadString(IMessagePackReader reader, uint length)
        {
            ArraySegment arraySegment = reader.ReadBytes(length);

            return Utf8.GetString(arraySegment.SourceBuffer, (int)arraySegment.SourceOffset + arraySegment.Position, (int)arraySegment.Length);

        }

        private static bool TryGetFixStrLength(DataTypes type, out uint length)
        {
            length = type - DataTypes.FixStr;
            return type.GetHighBits(3) == DataTypes.FixStr.GetHighBits(3);
        }

        private static void WriteStringHeaderAndLength(IMessagePackWriter writer, int length)
        {
            if (length <= 31)
            {
                writer.Write((byte)(((byte)DataTypes.FixStr + length) % 256));
                return;
            }

            if (length <= byte.MaxValue)
            {
                writer.Write(DataTypes.Str8);
                ((byte)length).WriteByteValue(writer);
            }
            else if (length <= ushort.MaxValue)
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
            Write((string)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
