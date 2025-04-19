// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.Json;
using nanoFramework.MessagePack.Benchmark.Base;
using nanoFramework.MessagePack.Benchmark.Data;

namespace nanoFramework.MessagePack.Benchmark.DeserializationBenchmarks
{
    [IterationCount(10)]
    internal class ComparativeDeserializationBenchmark : BaseIterationBenchmark
    {
        protected override int IterationCount => 1;

        [Benchmark]
        public void JsonDeserializationBenchmark()
        {
            RunInIteration(() =>
            {
                JsonConvert.DeserializeObject(ComparativeTestObjects.TestJson, typeof(TestObjectClass));
            });
        }

        [Benchmark]
        public void BinaryDeserializationBenchmark()
        {
            RunInIteration(() =>
            {
                BinaryFormatter.Deserialize(ComparativeTestObjects.TestObjectBinaryBytes);
            });
        }

        [Benchmark]
        public void MessagePackDeserializationBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(TestObjectClass), ComparativeTestObjects.TestObjectMsgPackBytes);
            });
        }
    }
}
