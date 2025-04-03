using nanoFramework.MessagePack.Stream;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
//    public interface IConverter<T> : IConverter
//    {
//#nullable enable
//        void Write(T? value, [NotNull] IMessagePackWriter writer);
//        new T? Read([NotNull] IMessagePackReader reader);
//    }

    public interface IConverter
    {
#nullable enable
        void Write(object? value, [NotNull] IMessagePackWriter writer);

        object? Read([NotNull] IMessagePackReader reader);
    }
}
