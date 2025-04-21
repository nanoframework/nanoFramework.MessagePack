// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using nanoFramework.Benchmark.Attributes;
using nanoFramework.Benchmark;
using nanoFramework.MessagePack.Benchmark.Base;
using nanoFramework.MessagePack.Benchmark.Data;

namespace nanoFramework.MessagePack.Benchmark.SerializationBenchmarks
{
    [IterationCount(5)]
    public class ReferenceTypesSerializationBenchmark : BaseIterationBenchmark
    {
        protected override int IterationCount => 20;
        [Benchmark]
        public void IntArrayBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ReferenceTestObjects.IntArray);
            });
        }

        [Benchmark]
        public void ByteArrayBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ReferenceTestObjects.ByteArray);
            });
        }

        [Benchmark]
        public void TwoDimensionalArrayBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ReferenceTestObjects.TwoDimensionalArray);
            });
        }

        [Benchmark]
        public void TestHashtableBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ReferenceTestObjects.TestHashtable);
            });
        }
    }
}
