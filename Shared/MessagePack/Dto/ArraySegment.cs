// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using nanoFramework.MessagePack.Utility;

namespace nanoFramework.MessagePack.Dto
{
    /// <summary>
    /// Segment by bytes array
    /// </summary>
    public class ArraySegment : IEnumerable, IEnumerator
    {
        private readonly byte[] _buffer;
        private readonly long _offset;
        private readonly long _length;

        /// <summary>
        /// Segment constructor
        /// </summary>
        /// <param name="buffer">Source byte array</param>
        /// <param name="offset">Offset in source byte array</param>
        /// <param name="length">Length segment</param>
        public ArraySegment(byte[] buffer, long offset, long length)
        {
            _buffer = buffer;
            _offset = offset;
            _length = length;
        }

        /// <summary>
        /// Rad one byte in segment
        /// </summary>
        /// <returns>Reading byte</returns>
        public byte ReadByte()
        {
            if (++Position >= _length)
            {
                throw ExceptionUtility.NotEnoughBytes(Position, _length);
            }

            if (_offset + Position >= _buffer.Length)
            {
                throw ExceptionUtility.NotEnoughBytes(_offset + Position, _buffer.Length);
            }

            return _buffer[_offset + Position];
        }

        /// <summary>
        /// Get bytes enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Go to next byte in array
        /// </summary>
        /// <returns>True if the end of the segment is not reached otherwise false</returns>
        public bool MoveNext()
        {
            Position++;

            return Position < _length && Position + _offset < _buffer.Length;
        }

        /// <summary>
        /// Resets the current position to the beginning of the segment
        /// </summary>
        public void Reset()
        {
            Position = -1;
        }

        /// <summary>
        /// Current position of the segment
        /// </summary>
        public long Position { get; private set; } = -1;

        /// <summary>
        /// The element corresponding to the current position in the segment
        /// </summary>
        public object Current
        {
            get
            {
                if (Position >= _length)
                {
                    throw ExceptionUtility.NotEnoughBytes(Position, _length);
                }

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
            System.Array.Copy(_buffer, (int)_offset, data, 0, (int)_length);
            return data;
        }

        /// <summary>
        /// Implicit conversion from byte array to <see cref="ArraySegment"/>.
        /// </summary>
        /// <param name="bytes"></param>
        public static implicit operator ArraySegment(byte[] bytes)
        {
            return new ArraySegment(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Implicit conversion from <see cref="ArraySegment"/> to byte array.
        /// </summary>
        /// <param name="segment"></param>
        public static explicit operator byte[](ArraySegment segment)
        {
            return segment.ToArray();
        }

    }
}
