using nanoFramework.MessagePack.Stream;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    /// <summary>
    /// Composite array converter
    /// </summary>
    public class ArrayConverter : IConverter
    {
        /// <summary>
        /// Write array in to MessagePack stream
        /// </summary>
        /// <param name="value">Array to by write</param>
        /// <param name="writer">MessagePack target stream</param>
        public void Write(IList value, IMessagePackWriter writer)
        {
            if (value == null || value.Count < 1)
            {
                ConverterContext.NullConverter.Write(value, writer);
                return;
            }

            writer.WriteArrayHeader((uint)value.Count);

            var elementType = value[0]!.GetType();
            var elementConverter = ConverterContext.GetConverter(elementType);
            if (elementConverter != null)
            {
                foreach (var element in value)
                {
                    elementConverter.Write(element, writer);
                }
            }
            else
            {
                foreach (var element in value)
                {
                    ConverterContext.SerializeObject(elementType, element, writer);
                }
            }

        }

        /// <summary>
        /// Read array from MessagePack stream
        /// </summary>
        /// <param name="reader">MessagePack stream for reading</param>
        /// <param name="arrayType">Target array type</param>
        /// <returns>Readied array</returns>
#nullable enable
        public static IList? Read(IMessagePackReader reader, Type arrayType)
        {
            var length = (int)reader.ReadArrayLength();
            return length > 0 ? ReadArray(reader, length, arrayType) : null;
        }

        private static Array ReadArray(IMessagePackReader reader, int length, Type arrayType)
        {
            var elementType = arrayType.GetElementType();
            var targetArray = (IList)Array.CreateInstance(elementType!, length);
            if (elementType!.IsArray)
            {
                for (var i = 0; i < length; i++)
                {
                    targetArray[i] = Read(reader, elementType);
                }
            }
            else
            {
                var converter = ConverterContext.GetConverter(elementType);
                if (converter != null)
                {
                    for (var i = 0; i < length; i++)
                    {
                        targetArray[i] = converter.Read(reader);
                    }
                }
                else
                {
                    for (var i = 0; i < length; i++)
                    {
                        targetArray[i] = ConverterContext.DeserializeObject(elementType, reader);
                    }
                }
            }

            return (Array) targetArray;
        }

        /// <summary>
        /// Write array in to MessagePack stream
        /// </summary>
        /// <param name="value">Array object to by write</param>
        /// <param name="writer">MessagePack target stream</param>
        public void Write(object value, [NotNull] IMessagePackWriter writer)
        {
            Write((IList)value, writer);
        }

        object IConverter.Read(IMessagePackReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
