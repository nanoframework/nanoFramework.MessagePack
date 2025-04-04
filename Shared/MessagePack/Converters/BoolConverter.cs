using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class BoolConverter : IConverter
    {
        private static void Write(bool value, IMessagePackWriter writer)
        {
            writer.Write(value ? DataTypes.True : DataTypes.False);
        }

        private static bool Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            return type switch
            {
                DataTypes.True => true,
                DataTypes.False => false,
                _ => throw ExceptionUtility.BadTypeException(type, DataTypes.True, DataTypes.False),
            };
        }

#nullable enable
        public void Write(object? value, [NotNull] IMessagePackWriter writer)
        {
            Write((bool)value!, writer);
        }

        object? IConverter.Read([NotNull] IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
