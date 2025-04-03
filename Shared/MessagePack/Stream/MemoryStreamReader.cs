using MessagePack.Dto;
using nanoFramework.MessagePack.Utility;
using System;
using System.Collections;
using System.IO;

namespace nanoFramework.MessagePack.Stream
{
    internal sealed class MemoryStreamReader : BaseReader, IDisposable
    {
        private readonly ArrayList _bytesGatheringBuffer = new();

        private bool _bytesGatheringInProgress;

        private readonly MemoryStream _stream;

        public MemoryStreamReader(MemoryStream stream)
        {
            _stream = stream;
        }

        public override byte ReadByte()
        {
            var temp = _stream.ReadByte();
            if (temp == -1)
            {
                throw ExceptionUtility.NotEnoughBytes(0, 1);
            }

            var result = (byte)temp;
            if (_bytesGatheringInProgress)
            {
                _bytesGatheringBuffer.Add(result);
            }

            return result;
        }

        public override ArraySegment ReadBytes(uint length)
        {
            var buffer = ReadBytesInternal(length);

            if (_bytesGatheringInProgress)
            {
                foreach (var b in buffer)
                {
                    _bytesGatheringBuffer.Add(b);
                }
            }

            return buffer;
        }

        public override void Seek(long offset, SeekOrigin origin)
        {
            if (_bytesGatheringInProgress)
            {
                var buffer = ReadBytesInternal((uint)offset);
                foreach (var b in buffer)
                {
                    _bytesGatheringBuffer.Add(b);
                }
            }
            else
            {
                _stream.Seek(offset, origin);
            }
        }

        public void Dispose()
        {
           _stream.Dispose();
        }

        private ArraySegment ReadBytesInternal(uint length)
        {
            var buffer = new byte[length];
            var read = _stream.Read(buffer, 0, buffer.Length);
            if (read < buffer.Length)
                throw ExceptionUtility.NotEnoughBytes(read, buffer.Length);
            return new(buffer, 0, buffer.Length);
        }
#nullable enable
        protected override ArraySegment? StopTokenGathering()
        {
            if ((_stream.Position + 1) <= _stream.Length)
            {
                _bytesGatheringInProgress = false;
                var result = _bytesGatheringBuffer.ToArray(typeof(byte[]));
                return new ArraySegment((byte[])result, 0, result.Length);
            }
            else
                return null;
        }

        protected override void StartTokenGathering()
        {
            _bytesGatheringInProgress = true;
            _bytesGatheringBuffer.Clear();
        }
    }
}
