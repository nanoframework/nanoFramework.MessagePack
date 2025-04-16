// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;

namespace nanoFramework.MessagePack.Converters
{
    internal class NullConverter : IConverter
    {
#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            writer.Write(DataTypes.Null);
        }

        public object? Read([NotNull] IMessagePackReader reader)
        {
            var type = reader.ReadDataType();
            if (type == DataTypes.Null)
            {
                return null;
            }

            throw ExceptionUtility.BadTypeException(type, DataTypes.Null);
        }
    }
}
