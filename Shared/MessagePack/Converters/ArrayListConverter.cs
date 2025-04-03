using nanoFramework.MessagePack.Stream;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    internal class ArrayListConverter : IConverter
    {
#nullable enable
        public void Write(ArrayList? value, IMessagePackWriter writer)
        {
            if (value == null)
            {
                ConverterContext.NullConverter.Write(value, writer);
                return;
            }

            writer.WriteArrayHeader((uint)value.Count);

            foreach (var element in value)
            {
                var elementType = element.GetType();
                var elementConverter = ConverterContext.GetConverter(elementType);
                if (elementConverter != null)
                    elementConverter.Write(element, writer);
                else
                    ConverterContext.SerializeObject(elementType, element, writer);
            }
        }

        public ArrayList? Read(IMessagePackReader reader)
        {
            var length = reader.ReadArrayLength();
            return ((long)length) > -1 ? ReadArrayList(reader, length) : null;
        }

        internal static ArrayList ReadArrayList(IMessagePackReader reader, uint length)
        {
            var array = new ArrayList();

            for (var i = 0; i < length; i++)
            {
                array.Add(ConverterContext.GetObjectByDataType(reader));
            }

            return array;
        }

        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((ArrayList)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            return Read(reader);
        }
    }
}
