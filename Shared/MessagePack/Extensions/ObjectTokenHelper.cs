// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using nanoFramework.MessagePack.Converters;
using nanoFramework.MessagePack.Dto;
using nanoFramework.MessagePack.Exceptions;
using nanoFramework.MessagePack.Stream;

namespace nanoFramework.MessagePack.Extensions
{
    internal static class ObjectTokenHelper
    {
#nullable enable
        internal static Hashtable? GetMessagePackObjectTokens([NotNull] this IMessagePackReader reader)
        {
            int length = (int)reader.ReadMapLength();
            return length > 0 ? reader.ReadMap(length) : null;
        }

        private static Hashtable ReadMap(this IMessagePackReader reader, int length)
        {
            var map = new Hashtable(length);

            IConverter stringConverter = ConverterContext.GetConverter(typeof(string));
            while (length-- > 0)
            {
                string key = (stringConverter.Read(reader) as string) ?? throw new SerializationException("Map key must be a string.");
                ArraySegment? value = reader.ReadToken();

                map[key] = value;
            }

            return map;
        }
    }
}
