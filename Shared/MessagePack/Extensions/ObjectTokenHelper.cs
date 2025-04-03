using nanoFramework.MessagePack.Stream;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Extensions
{
    internal static class ObjectTokenHelper
    {
#nullable enable
        internal static Hashtable? GetMassagePackObjectTokens([NotNull] this IMessagePackReader reader)
        {
            var length = reader.ReadMapLength();
            return length > 0 ? reader.ReadMap(length) : null;
        }

        private static Hashtable ReadMap(this IMessagePackReader reader, uint length)
        {
            var map = new Hashtable();

            var stringConverter = ConverterContext.GetConverter(typeof(string));
            for (var i = 0; i < length; i++)
            {
                var key = stringConverter.Read(reader);
                var value = reader.ReadToken();

                map[key!] = value;
            }

            return map;
        }
    }
}
