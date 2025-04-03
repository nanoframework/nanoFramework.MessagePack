using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
using System.IO;
using System.Collections;

namespace MessagePack.Dto
{
    public class ArraySegment : IEnumerable, IEnumerator
    {
        private readonly byte[] _buffer;
        private readonly long _offset;
        private readonly long _length;
        public ArraySegment(byte[] buffer,  long offset, long length)
        {
            _buffer = buffer;
            _offset = offset;
            _length = length;
        }

        public byte ReadByte()
        {
            if (++Position >= _length)
                throw ExceptionUtility.NotEnoughBytes(Position, _length);
            if (_offset + Position >= _buffer.Length)
                throw ExceptionUtility.NotEnoughBytes(_offset + Position, _buffer.Length);
            return _buffer[_offset + Position];
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            Position++;

            return Position < _length && Position + _offset < _buffer.Length;
        }

        public void Reset()
        {
            Position = -1;
        }

        public long Position { get; private set; } = -1;

        public object Current
        {
            get
            {
                if (Position >= _length)
                    throw ExceptionUtility.NotEnoughBytes(Position, _length);
                try
                {
                    return _buffer[_offset + Position];
                }
                catch
                {
                    throw ExceptionUtility.NotEnoughBytes(_offset + Position, _buffer.Length);
                }
            }
        }

        private byte[] ToArray()
        {
            var data = new byte[_length];
            //for (int i = 0; i < data.Length; i++)
            //{
            //    data[i] = _buffer[_offset + i];
            //}
            System.Array.Copy(_buffer, (int) _offset, data, 0, (int) _length);
            return data;
        }

        public static implicit operator ArraySegment(byte[] bytes)
        {
            return new ArraySegment(bytes, 0, bytes.Length);
        }

        public static explicit operator byte[](ArraySegment segment)
        {
            return segment.ToArray();
        }

    }
}
