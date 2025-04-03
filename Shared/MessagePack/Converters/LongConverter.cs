using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class LongConverter : IConverter
    {
        public void Write(long value, IMessagePackWriter writer)
        {
            NumberConverterHelper.WriteInteger(value, writer);
        }

        public long Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            if (NumberConverterHelper.TryGetInt64(type, reader, out var result))
                return result;
            else
                throw ExceptionUtility.IntDeserializationFailure(type);
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((long)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
