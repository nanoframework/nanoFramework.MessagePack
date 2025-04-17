// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using nanoFramework.MessagePack;
using nanoFramework.MessagePack.Converters;
using nanoFramework.MessagePack.Stream;
using System;

namespace UnitTestShared.TestData
{
    public class SecureMessageConverter : IConverter
    {
        public SecureMessage Read([NotNull] IMessagePackReader reader)
        {
            StringBuilder sb = new();
            var length = reader.ReadArrayLength();

            for (int i = 0; i < length; i++)
            {
                sb.Append(SharedWordDictionary.WordDictionary[i]);
                sb.Append(' ');
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            return new SecureMessage(sb.ToString());
        }

        public void Write(SecureMessage value, [NotNull] IMessagePackWriter writer)
        {
            var messageWords = value.Message.Split(' ');

            uint length = BitConverter.ToUInt32(BitConverter.GetBytes(messageWords.Length), 0);
            writer.WriteArrayHeader(length);

            var intConverter = ConverterContext.GetConverter(typeof(int));

            foreach (var word in messageWords)
            {
                intConverter.Write(SharedWordDictionary.WordDictionary.IndexOf(word), writer);
            }
        }

#nullable enable
        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }

        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((SecureMessage)value!, writer);
        }
    }
}
