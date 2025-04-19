// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using nanoFramework.Benchmark;
using System.Collections;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.MessagePack.Benchmark.Base;
using nanoFramework.MessagePack.Benchmark.Data;

namespace nanoFramework.MessagePack.Benchmark.DeserializationBenchmarks
{
    [IterationCount(5)]
    internal class ReferenceTypesDeserializationBenchmark : BaseIterationBenchmark
    {
        protected override int IterationCount => 20;
        private byte[] IntArrayBytes;
        private byte[] ByteArrayBytes;
        private byte[] TwoDimensionalArrayBytes;
        private byte[] TestHashtableBytes;

        [Setup]
        public void Setup()
        {
            IntArrayBytes = MessagePackSerializer.Serialize(ReferenceTestObjects.IntArray);
            ByteArrayBytes = MessagePackSerializer.Serialize(ReferenceTestObjects.ByteArray);
            TwoDimensionalArrayBytes = MessagePackSerializer.Serialize(ReferenceTestObjects.TwoDimensionalArray);
            TestHashtableBytes = MessagePackSerializer.Serialize(ReferenceTestObjects.TestHashtable);
        }

        [Benchmark]
        public void IntArrayBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(int[]), IntArrayBytes);
            });
        }

        [Benchmark]
        public void ByteArrayBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(byte[]), ByteArrayBytes);
            });
        }

        [Benchmark]
        public void TwoDimensionalArrayBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(long[][]), TwoDimensionalArrayBytes);
            });
        }

        [Benchmark]
        public void TestHashtableBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(Hashtable), TestHashtableBytes);
            });
        }
    }
}
