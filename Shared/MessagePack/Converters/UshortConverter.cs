// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;

namespace nanoFramework.MessagePack.Converters
{
    internal class UshortConverter : IConverter
    {
        private static void Write(ushort value, IMessagePackWriter writer)
        {
            NumberConverterHelper.WriteNonNegativeInteger(value, writer);
        }

        private static ushort Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetFixPositiveNumber(type, out byte temp))
            {
                return temp;
            }

            if (NumberConverterHelper.TryGetNegativeNumber(type, out sbyte tempInt8))
            {
                return (ushort)tempInt8;
            }

            return type switch
            {
                DataTypes.UInt8 => NumberConverterHelper.ReadUInt8(reader),
                DataTypes.UInt16 => NumberConverterHelper.ReadUInt16(reader),
                DataTypes.Int8 => (ushort)NumberConverterHelper.ReadInt8(reader),
                DataTypes.Int16 => (ushort)NumberConverterHelper.ReadInt16(reader),
                _ => throw ExceptionUtility.IntDeserializationFailure(type),
            };
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((ushort)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
