using nanoFramework.MessagePack.Converters;
using nanoFramework.MessagePack.Stream;
using System;
using System.Diagnostics.CodeAnalysis;

namespace UnitTestShared.TestData
{
    internal class TestConverter : IConverter
    {
#nullable enable
        public object? Read([NotNull] IMessagePackReader reader)
        {
            throw new NotImplementedException();
        }

        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
