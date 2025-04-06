using nanoFramework.MessagePack.Dto;
using nanoFramework.MessagePack.Converters;
using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Utility;
using System.IO;

namespace nanoFramework.MessagePack.Stream
{
    internal abstract class BaseReader : IMessagePackReader
    {
        public abstract byte ReadByte();

        public abstract ArraySegment ReadBytes(uint length);

        public abstract void Seek(long offset, SeekOrigin origin);

        public uint ReadArrayLength()
        {
            var type = ReadDataType();
            switch (type)
            {
                case DataTypes.Null:
                    return uint.MaxValue;
                case DataTypes.Array16:
                    return NumberConverterHelper.ReadUInt16(this);

                case DataTypes.Array32:
                    return NumberConverterHelper.ReadUInt32(this);
            }

            if (TryGetLengthFromFixArray(type, out var length))
            {
                return length;
            }

            throw ExceptionUtility.BadTypeException(type, DataTypes.Array16, DataTypes.Array32, DataTypes.FixArray, DataTypes.Null);
        }

        public uint ReadMapLength()
        {
            var type = ReadDataType();

            switch (type)
            {
                case DataTypes.Null:
                    return uint.MaxValue;
                case DataTypes.Map16:
                    return NumberConverterHelper.ReadUInt16(this);

                case DataTypes.Map32:
                    return NumberConverterHelper.ReadUInt32(this);
            }

            if (TryGetLengthFromFixMap(type, out var length))
                return length;

            throw ExceptionUtility.BadTypeException(type, DataTypes.Map16, DataTypes.Map32, DataTypes.FixMap, DataTypes.Null);
        }

        public virtual DataTypes ReadDataType()
        {
            return (DataTypes)ReadByte();
        }

        public void SkipToken()
        {
            var dataType = ReadDataType();

            switch (dataType)
            {
                case DataTypes.Null:
                case DataTypes.False:
                case DataTypes.True:
                    return;
                case DataTypes.UInt8:
                case DataTypes.Int8:
                    SkipBytes(1);
                    return;
                case DataTypes.UInt16:
                case DataTypes.Int16:
                    SkipBytes(2);
                    return;
                case DataTypes.UInt32:
                case DataTypes.Int32:
                case DataTypes.Single:
                    SkipBytes(4);
                    return;
                case DataTypes.UInt64:
                case DataTypes.Int64:
                case DataTypes.Double:
                    SkipBytes(8);
                    return;
                case DataTypes.Array16:
                    SkipArrayItems(NumberConverterHelper.ReadUInt16(this));
                    return;
                case DataTypes.Array32:
                    SkipArrayItems(NumberConverterHelper.ReadUInt32(this));
                    return;
                case DataTypes.Map16:
                    SkipMapItems(NumberConverterHelper.ReadUInt16(this));
                    return;
                case DataTypes.Map32:
                    SkipMapItems(NumberConverterHelper.ReadUInt32(this));
                    return;
                case DataTypes.Str8:
                    SkipBytes(NumberConverterHelper.ReadUInt8(this));
                    return;
                case DataTypes.Str16:
                    SkipBytes(NumberConverterHelper.ReadUInt16(this));
                    return;
                case DataTypes.Str32:
                    SkipBytes(NumberConverterHelper.ReadUInt32(this));
                    return;
                case DataTypes.Bin8:
                    SkipBytes(NumberConverterHelper.ReadUInt8(this));
                    return;
                case DataTypes.Bin16:
                    SkipBytes(NumberConverterHelper.ReadUInt16(this));
                    return;
                case DataTypes.Bin32:
                    SkipBytes(NumberConverterHelper.ReadUInt32(this));
                    return;
            }

            if (dataType.GetHighBits(1) == DataTypes.PositiveFixNum.GetHighBits(1) ||
                dataType.GetHighBits(3) == DataTypes.NegativeFixNum.GetHighBits(3))
            {
                return;
            }

            if (TryGetLengthFromFixArray(dataType, out var arrayLength))
            {
                SkipArrayItems(arrayLength);
                return;
            }

            if (TryGetLengthFromFixMap(dataType, out var mapLength))
            {
                SkipMapItems(mapLength);
                return;
            }
            if (TryGetLengthFromFixStr(dataType, out var stringLength))
            {
                SkipBytes(stringLength);
                return;
            }

            throw new System.ArgumentOutOfRangeException();
        }
#nullable enable
        public ArraySegment? ReadToken()
        {
            StartTokenGathering();
            SkipToken();
            var gatheredBytes = StopTokenGathering();
            return gatheredBytes;
        }

        private void SkipMapItems(uint count)
        {
            while(count > 0)
            {
                SkipToken();
                SkipToken();
                count--;
            }
        }

        private void SkipArrayItems(uint count)
        {
            while (count > 0)
            {
                SkipToken();
                count--;
            }
        }

        private void SkipBytes(uint bytesCount)
        {
            Seek(bytesCount, SeekOrigin.Current);
        }

        private static bool TryGetLengthFromFixStr(DataTypes type, out uint length)
        {
            length = type - DataTypes.FixStr;
            return type.GetHighBits(3) == DataTypes.FixStr.GetHighBits(3);
        }

        protected static bool TryGetLengthFromFixArray(DataTypes type, out uint length)
        {
            length = type - DataTypes.FixArray;
            return type.GetHighBits(4) == DataTypes.FixArray.GetHighBits(4);
        }

        protected static bool TryGetLengthFromFixMap(DataTypes type, out uint length)
        {
            length = type - DataTypes.FixMap;
            return type.GetHighBits(4) == DataTypes.FixMap.GetHighBits(4);
        }

        protected abstract ArraySegment? StopTokenGathering();

        protected abstract void StartTokenGathering();
    }
}
