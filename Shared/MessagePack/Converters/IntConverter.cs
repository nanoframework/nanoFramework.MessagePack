using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class IntConverter : IConverter
    {
        private static void Write(int value, IMessagePackWriter writer)
        {
            NumberConverterHelper.WriteInteger(value, writer);
        }

        private static int Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetInt32(type, reader, out var result))
                return result;
            else
                throw ExceptionUtility.IntDeserializationFailure(type);
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((int)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
