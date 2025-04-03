using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;

namespace nanoFramework.MessagePack.Converters
{
    internal class NullConverter : IConverter
    {
        public void Write(object value, IMessagePackWriter writer)
        {
            writer.Write(DataTypes.Null);
        }

        public object Read(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();
            if (type == DataTypes.Null)
                return null;

            throw ExceptionUtility.BadTypeException(type, DataTypes.Null);
        }
    }
}
