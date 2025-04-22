﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using nanoFramework.MessagePack.Converters;
using nanoFramework.MessagePack.Dto;
using nanoFramework.MessagePack.Exceptions;
using nanoFramework.MessagePack.Extensions;
using nanoFramework.MessagePack.Stream;
using nanoFramework.MessagePack.Utility;
#if !NANOFRAMEWORK_1_0
using System.Collections.Concurrent;
#endif

namespace nanoFramework.MessagePack
{
    /// <summary>
    /// Context by serialization/deserialization
    /// </summary>
    public static class ConverterContext
    {
        private static readonly Type[] _emptyTypes = new Type[0];
        private static readonly NullConverter s_nullConverter = new();
        private static readonly MapConverter s_mapConverter = new();
        private static readonly ArrayConverter s_arrayConverter = new();

#if NANOFRAMEWORK_1_0
        private static readonly Hashtable _mappingDictionary = new();
#else
        private static readonly ConcurrentDictionary<Type, MemberMapping[]> _mappingDictionary = new();
#endif

        private static readonly Hashtable ConversionTable = new()
        {
            { typeof(IDictionary).FullName!, s_mapConverter },
            { typeof(Hashtable).FullName!, s_mapConverter },
            { typeof(ArrayList).FullName!,  new ArrayListConverter()},
            { typeof(short).FullName!, new ShortConverter() },
            { typeof(ushort).FullName!, new UshortConverter() },
            { typeof(int).FullName!, new IntConverter() },
            { typeof(uint).FullName!, new UintConverter() },
            { typeof(long).FullName!, new LongConverter() },
            { typeof(ulong).FullName!, new UlongConverter() },
            { typeof(byte).FullName!, new ByteConverter() },
            { typeof(sbyte).FullName!, new SbyteConverter() },
            { typeof(float).FullName!, new FloatConverter()},
            { typeof(double).FullName!, new DoubleConverter() },
            { typeof(bool).FullName!, new BoolConverter() },
            { typeof(string).FullName!, new StringConverter() },
            { typeof(TimeSpan).FullName!, new TimeSpanConverter() },
            { typeof(DateTime).FullName!, new DateTimeConverter() },
            { typeof(char).FullName!, new CharConverter() },
            { typeof(Guid).FullName!, new GuidConverter() },
            { typeof(byte[]).FullName!, new BinaryConverter() },
            { typeof(int[]).FullName!, new SimpleArrayConverter(typeof(int))},
            { typeof(uint[]).FullName!, new SimpleArrayConverter(typeof(uint))},
            { typeof(long[]).FullName!, new SimpleArrayConverter(typeof(long))},
            { typeof(ulong[]).FullName!, new SimpleArrayConverter(typeof(ulong))},
            { typeof(float[]).FullName!, new SimpleArrayConverter(typeof(float)) },
            { typeof(double[]).FullName!, new SimpleArrayConverter(typeof(double)) },
            { typeof(char[]).FullName!, new SimpleArrayConverter(typeof(char)) },
            { typeof(string[]).FullName!, new SimpleArrayConverter(typeof(string)) },
            { typeof(Guid[]).FullName!, new SimpleArrayConverter(typeof(Guid)) },
            { typeof(sbyte[]).FullName!, new SimpleArrayConverter(typeof(sbyte)) },
            { typeof(DateTime[]).FullName!, new SimpleArrayConverter(typeof(DateTime)) },
            { typeof(TimeSpan[]).FullName!, new SimpleArrayConverter(typeof(TimeSpan)) },
            { typeof(bool[]).FullName!, new SimpleArrayConverter(typeof(bool)) },
            { typeof(short[]).FullName!, new SimpleArrayConverter(typeof(short)) },
            { typeof(ushort[]).FullName!, new SimpleArrayConverter(typeof(ushort)) }
        };

        /// <summary>
        /// Null value converter
        /// </summary>
        public static IConverter NullConverter => s_nullConverter;

        /// <summary>
        /// Adds new converter to collection to support more types.
        /// </summary>
        /// <param name="type">Type of object.</param>
        /// <param name="converter">Converter instance which will be used to convert <paramref name="type"/></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Add(Type type, IConverter converter)
        {
            if (type == typeof(object))
            {
                throw new NotSupportedException($"Converter by type {type.Name} not support in convertors table.");
            }

            ConversionTable.Add(type.FullName!, converter);
        }

        /// <summary>
        /// Remove existing type converter.
        /// </summary>
        /// <param name="type">Type of object.</param>
        public static void Remove(Type type)
        {
            ConversionTable.Remove(type.FullName!);
        }

        /// <summary>
        /// Remove and then adds converter for given type.
        /// </summary>
        /// <param name="type">Type of object.</param>
        /// <param name="converter">Converter instance which will be used to convert <paramref name="type"/></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Replace(Type type, IConverter converter)
        {
            Remove(type);
            Add(type, converter);
        }

        /// <summary>
        /// Return converter by type
        /// </summary>
        /// <param name="type">Type from converter</param>
        /// <returns>Converter interface <see cref="IConverter"/></returns>
        public static IConverter GetConverter(Type type)
        {
            if (type == typeof(object))
            {
                throw ExceptionUtility.ConverterNotFound(type);
            }

            if (ConversionTable.Contains(type.FullName!))
            {
                return (IConverter)ConversionTable[type.FullName!]!;
            }

            return null!;
        }

        internal static Hashtable GetMappingsValues(Type targetType, object value)
        {
            var memberMappings = GetMemberMapping(targetType);

            Hashtable result = new();

            foreach (var memberMapping in memberMappings)
            {
#if NANOFRAMEWORK_1_0
                if(!memberMapping.OriginalName!.StartsWith(MemberMapping.SET_))
                {
#endif
                if (memberMapping.TryGetValue(value, out var memberValue) && memberValue != null)
                {
                    result.Add(memberMapping.Name!, memberValue);
                }
#if NANOFRAMEWORK_1_0
                }
#endif
            }
            return result;
        }

        internal static void SetMappingsValues(Type targetType, object targetObject, Hashtable objectValuesMap)
        {
            var memberMappings = GetMemberMapping(targetType);

            foreach (var memberMapping in memberMappings)
            {
#if NANOFRAMEWORK_1_0
                if(!memberMapping.OriginalName!.StartsWith(MemberMapping.GET_))
                {
#endif
                if (objectValuesMap.Contains(memberMapping.Name!))
                {
                    var memberMpToken = (ArraySegment)objectValuesMap[memberMapping.Name!]!;
                    if (memberMpToken != null)
                    {
                        var memberValueMapType = memberMapping.GetMemberType();
                        var converter = GetConverter(memberValueMapType!);
                        if (converter != null)
                        {
                            memberMapping.SetValue(targetObject, converter.Read(memberMpToken)!);
                        }
                        else
                        {
                            if (memberValueMapType!.IsArray)
                            {
                                memberMapping.SetValue(targetObject, ArrayConverter.Read(memberMpToken, memberValueMapType)!);
                            }
                            else
                            {

                                memberMapping.SetValue(targetObject, DeserializeObject(memberValueMapType!, memberMpToken)!);
                            }
                        }
                    }
                }
#if NANOFRAMEWORK_1_0
                }
#endif
            }
        }

        internal static object CreateInstance(Type targetType)
        {
            var constructor = targetType.GetConstructor(_emptyTypes) ?? throw new Exception($"Target type {targetType?.FullName} does not have a parameterless constructor");
            return constructor.Invoke(null);
        }

        internal static MemberMapping[] GetMemberMapping(Type targetType)
        {
#if NANOFRAMEWORK_1_0
            var cached = _mappingDictionary[targetType];
            if (cached is not MemberMapping[] memberMappings)
            {
#else
            if (!_mappingDictionary.TryGetValue(targetType, out var memberMappings))
            {
#endif
                var mappings = new ArrayList();

                FieldUtility.Map(targetType, mappings);
                PropertyUtility.Map(targetType, mappings);

                memberMappings = (MemberMapping[])mappings.ToArray(typeof(MemberMapping));

#if NANOFRAMEWORK_1_0
                ThreadSafeAddItemCache(_mappingDictionary, targetType, memberMappings);
#else
                _mappingDictionary.TryAdd(targetType, memberMappings);
#endif
            }

            return memberMappings;
        }

        internal static void SerializeObject(Type type, object value, IMessagePackWriter writer)
        {
            if (value is IDictionary || value is Hashtable)
            {
                s_mapConverter.Write(value, writer);
                return;
            }

            if (type.IsArray)
            {
                s_arrayConverter.Write(value, writer);
                return;
            }

            var objectMap = GetMappingsValues(type, value);
            s_mapConverter.Write(objectMap, writer);

        }
#nullable enable
        internal static object? DeserializeObject(Type type, IMessagePackReader reader)
        {
            if (type.Name == typeof(IDictionary).Name || type.Name == typeof(Hashtable).Name)
            {
                return MapConverter.Read(reader);
            }

            if (type.IsArray)
            {
                return ArrayConverter.Read(reader, type);
            }

            var objectMap = reader.GetMassagePackObjectTokens();
            if (objectMap != null && objectMap is Hashtable targetObjectMap)
            {
                var targetObject = CreateInstance(type);
                SetMappingsValues(type, targetObject, targetObjectMap);
                return targetObject;
            }
            else
            {
                throw new SerializationException($"Type {type.Name} can not by deserialize.");
            }
        }

        internal static object? GetObjectByDataType(IMessagePackReader reader)
        {
            var type = reader.ReadDataType();

            return type switch
            {
                DataTypes.Null => null,
                DataTypes.True => true,
                DataTypes.False => false,
                DataTypes.Double => NumberConverterHelper.ReadDouble(reader),
                DataTypes.Single => NumberConverterHelper.ReadFloat(reader),
                DataTypes.Str8 => StringConverter.ReadString(reader, NumberConverterHelper.ReadUInt8(reader)),
                DataTypes.Str16 => StringConverter.ReadString(reader, NumberConverterHelper.ReadUInt16(reader)),
                DataTypes.Str32 => StringConverter.ReadString(reader, NumberConverterHelper.ReadUInt32(reader)),
                DataTypes.UInt8 => NumberConverterHelper.ReadUInt8(reader),
                DataTypes.Int8 => (byte)NumberConverterHelper.ReadInt8(reader),
                DataTypes.UInt16 => NumberConverterHelper.ReadUInt16(reader),
                DataTypes.Int16 => NumberConverterHelper.ReadInt16(reader),
                DataTypes.UInt32 => NumberConverterHelper.ReadUInt32(reader),
                DataTypes.Int32 => NumberConverterHelper.ReadInt32(reader),
                DataTypes.UInt64 => NumberConverterHelper.ReadUInt64(reader),
                DataTypes.Int64 => NumberConverterHelper.ReadInt64(reader),
                DataTypes.Bin8 => reader.ReadBytes(NumberConverterHelper.ReadUInt8(reader)),
                DataTypes.Bin16 => reader.ReadBytes(NumberConverterHelper.ReadUInt16(reader)),
                DataTypes.Bin32 => reader.ReadBytes(NumberConverterHelper.ReadUInt32(reader)),
                DataTypes.Timestamp32 => ReadDateTimeExt(type, reader),
                DataTypes.Timestamp64 => ReadDateTimeExt(type, reader),
                DataTypes.Timestamp96 => ReadDateTimeExt(type, reader),
                DataTypes.Array16 => ArrayListConverter.ReadArrayList(reader, NumberConverterHelper.ReadUInt16(reader)),
                DataTypes.Array32 => ArrayListConverter.ReadArrayList(reader, NumberConverterHelper.ReadUInt32(reader)),
                DataTypes.Map16 => MapConverter.ReadMap(reader, NumberConverterHelper.ReadUInt16(reader)),
                DataTypes.Map32 => MapConverter.ReadMap(reader, NumberConverterHelper.ReadUInt32(reader)),
                _ => ReadObject(type, reader),
            };
        }

#if NANOFRAMEWORK_1_0
        private static void ThreadSafeAddItemCache(Hashtable hashtable, object key, object value)
        {
            if (!hashtable.Contains(key))
            {
                lock(_mappingDictionary)
                {
                    try
                    {
                         if (!hashtable.Contains(key))
                         {
                            hashtable.Add(key, value);
                         }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error added key: '{key}', value: '{value}'\r\n{ex}");
                    }
                }
            }
        }
#endif
        private static object ReadObject(DataTypes type, IMessagePackReader reader)
        {
            if (type.GetHighBits(3) == DataTypes.FixStr.GetHighBits(3))
            {
                return StringConverter.ReadString(reader, type - DataTypes.FixStr);
            }

            if (NumberConverterHelper.TryGetFixPositiveNumber(type, out var positive))
            {
                return positive;
            }

            if (NumberConverterHelper.TryGetNegativeNumber(type, out var negative))
            {
                return negative;
            }

            if (type.GetHighBits(4) == DataTypes.FixArray.GetHighBits(4))
            {
                return ArrayListConverter.ReadArrayList(reader, type - DataTypes.FixArray);
            }

            if (type.GetHighBits(4) == DataTypes.FixMap.GetHighBits(4))
            {
                return MapConverter.ReadMap(reader, type - DataTypes.FixMap);
            }

            throw ExceptionUtility.BadTypeException(type);
        }

        private static DateTime ReadDateTimeExt(DataTypes type, IMessagePackReader reader)
        {
            uint nanoSeconds = 0;
            sbyte extType = NumberConverterHelper.ReadInt8(reader);
            long seconds;
            if (extType == -1)
            {
                if (type == DataTypes.Timestamp32)
                {
                    seconds = NumberConverterHelper.ReadUInt32(reader);
                }
                else
                {
                    nanoSeconds = NumberConverterHelper.ReadUInt32(reader);
                    seconds = NumberConverterHelper.ReadUInt32(reader);
                }
            }
            else if (extType == 12)
            {
                extType = NumberConverterHelper.ReadInt8(reader);
                if (extType == -1)
                {
                    nanoSeconds = NumberConverterHelper.ReadUInt32(reader);
                    seconds = NumberConverterHelper.ReadInt64(reader);
                }
                else
                {
                    throw ExceptionUtility.BadTypeException(type, DataTypes.Timestamp32, DataTypes.Timestamp64, DataTypes.Timestamp96);
                }
            }
            else
            {
                throw ExceptionUtility.BadTypeException(type, DataTypes.Timestamp32, DataTypes.Timestamp64, DataTypes.Timestamp96);
            }

            return DateTime.UnixEpoch.AddSeconds(seconds).AddMilliseconds(nanoSeconds / 1000.0);
        }
    }
}

