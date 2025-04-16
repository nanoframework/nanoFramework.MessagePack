// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics.CodeAnalysis;
using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;

namespace nanoFramework.MessagePack.Converters
{
    internal class FloatConverter : IConverter
    {
        private static void Write(float value, IMessagePackWriter writer)
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

        private static float Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (type == DataTypes.Single)
            {
                return NumberConverterHelper.ReadFloat(reader);
            }

            if (NumberConverterHelper.TryGetInt32(type, reader, out var result))
            {
                return result;
            }
            else
            {
                throw ExceptionUtility.BadTypeException(type, DataTypes.Single);
            }
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((float)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
