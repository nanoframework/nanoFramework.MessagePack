// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#if NANOFRAMEWORK_1_0
using System;
using System.IO;
#endif
using nanoFramework.MessagePack.Dto;
using nanoFramework.MessagePack.Utility;

namespace nanoFramework.MessagePack.Stream
{
    internal sealed class MemoryStreamReader : BaseReader, IDisposable
    {
        private readonly MemoryStream _stream;
        private long _gatheringStartPosition;

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

            return (byte)temp;
        }

        public override ArraySegment ReadBytes(uint length)
        {
            var buffer = ReadBytesInternal(length);
            return buffer;
        }

        public override void Seek(long offset, SeekOrigin origin)
        {
            _stream.Seek(offset, origin);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        private byte[] ReadBytesInternal(uint length)
        {
            var buffer = new byte[length];
            var read = _stream.Read(buffer, 0, buffer.Length);
            if (read < buffer.Length)
            {
                throw ExceptionUtility.NotEnoughBytes(read, buffer.Length);
            }

            return buffer;
        }
#nullable enable
        protected override ArraySegment? StopTokenGathering()
        {
            if (_stream.Position <= _stream.Length)
            {
                long currentPosition = _stream.Position;
                long bytesGathered = currentPosition - _gatheringStartPosition;
                
                byte[] result = new byte[bytesGathered];
                
                _stream.Position = _gatheringStartPosition;
                _stream.Read(result, 0, result.Length);
                _stream.Position = currentPosition;

                return new ArraySegment(result, 0, result.Length);
            }
            else
            {
                return null;
            }
        }

        protected override void StartTokenGathering()
        {
            _gatheringStartPosition = _stream.Position;
        }
    }
}
