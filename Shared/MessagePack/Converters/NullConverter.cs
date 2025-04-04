using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

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
                return null;

            throw ExceptionUtility.BadTypeException(type, DataTypes.Null);
        }
    }
}
