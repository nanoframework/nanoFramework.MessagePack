using nanoFramework.MessagePack.Stream;
using System.Diagnostics.CodeAnalysis;

namespace nanoFramework.MessagePack.Converters
{
    /// <summary>
    /// Converter interface
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Write object in to target MessagePack stream
        /// </summary>
        /// <param name="value">Source object</param>
        /// <param name="writer">Target MessagePack stream</param>
#nullable enable
        void Write(object? value, [NotNull] IMessagePackWriter writer);

        /// <summary>
        /// Read object from MessagePack stream
        /// </summary>
        /// <param name="reader">MessagePack stream</param>
        /// <returns>Readied object</returns>
        object? Read([NotNull] IMessagePackReader reader);
    }
}
