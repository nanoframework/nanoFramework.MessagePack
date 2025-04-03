using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class IntConverter : IConverter
    {
        public void Write(int value, IMessagePackWriter writer)
        {
            NumberConverterHelper.WriteInteger(value, writer);
        }

        public int Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetInt32(type, reader, out var result))
                return result;
            else
                throw ExceptionUtility.IntDeserializationFailure(type);
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((int)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
