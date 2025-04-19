// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections;

namespace nanoFramework.MessagePack.Benchmark.Data
{
    internal class ReferenceTestObjects
    {
        static ReferenceTestObjects()
        {
            var random = new Random();

            for (int i = 0; i < IntArray.Length; i++)
            {
                IntArray[i] = random.Next(int.MaxValue);
            }

            for (int i = 0; i < IntArray.Length; i++)
            {
                ByteArray[i] = (byte)random.Next(byte.MaxValue);
            }

            for (int x = 0; x < TwoDimensionalArray.Length; x++)
            {
                TwoDimensionalArray[x] = new long[10];
                for (int y = 0; y < TwoDimensionalArray[x].Length; y++)
                {
                    TwoDimensionalArray[x][y] = (long)random.Next(int.MaxValue) * int.MaxValue;
                }
            }

            TestHashtable.Add("gender", "Male");
            TestHashtable.Add("snow", "white");
            TestHashtable.Add(1, "one");
            TestHashtable.Add("two", 2);
            TestHashtable.Add(Guid.NewGuid(), new string[] { "nano,", "Framework", "Message", "Pack" });
        }
        internal static int[] IntArray { get; } = new int[100];
        internal static byte[] ByteArray { get; } = new byte[100];
        internal static long[][] TwoDimensionalArray { get; } = new long[10][];
        internal static Hashtable TestHashtable { get; } = new();
    }
}
