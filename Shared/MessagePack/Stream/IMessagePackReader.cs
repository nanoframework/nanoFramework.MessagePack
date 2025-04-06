using nanoFramework.MessagePack.Dto;
using System.IO;

namespace nanoFramework.MessagePack.Stream
{
    public interface IMessagePackReader
    {
        DataTypes ReadDataType();

        byte ReadByte();

        ArraySegment ReadBytes(uint length);

        void Seek(long offset, SeekOrigin origin);

        uint ReadArrayLength();

        uint ReadMapLength();

        void SkipToken();
#nullable enable
        ArraySegment? ReadToken();
    }
}
