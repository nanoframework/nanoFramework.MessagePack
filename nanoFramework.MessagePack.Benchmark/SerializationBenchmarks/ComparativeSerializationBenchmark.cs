// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using nanoFramework.Benchmark;
using System.Runtime.Serialization.Formatters.Binary;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.Json;
using nanoFramework.MessagePack.Benchmark.Base;
using nanoFramework.MessagePack.Benchmark.Data;

namespace nanoFramework.MessagePack.Benchmark.SerializationBenchmarks
{
    [IterationCount(10)]
    internal class ComparativeSerializationBenchmark : BaseIterationBenchmark
    {
        protected override int IterationCount => 1;

        [Benchmark]
        public void JsonSerializationBenchmark()
        {
            RunInIteration(() =>
            {
                JsonConvert.SerializeObject(ComparativeTestObjects.TestObject);
            });
        }

        [Benchmark]
        public void BinarySerializationBenchmark()
        {
            RunInIteration(() =>
            {
                BinaryFormatter.Serialize(ComparativeTestObjects.TestObject);
            });
        }

        [Benchmark]
        public void MessagePackSerializationBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ComparativeTestObjects.TestObject);
            });
        }
    }
}
