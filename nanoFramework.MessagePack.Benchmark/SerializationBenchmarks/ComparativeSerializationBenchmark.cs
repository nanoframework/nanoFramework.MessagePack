// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.MessagePack.Benchmark.SerializationBenchmarks
{
    using System.Runtime.Serialization.Formatters.Binary;
    using nanoFramework.Benchmark;
    using nanoFramework.Benchmark.Attributes;
    using nanoFramework.Json;
    using nanoFramework.MessagePack.Benchmark.Base;
    using nanoFramework.MessagePack.Benchmark.Data;

    /// <summary>
    /// Comparative serialization benchmark.
    /// </summary>
    [IterationCount(10)]
    public class ComparativeSerializationBenchmark : BaseIterationBenchmark
    {
        /// <summary>
        /// public iteration count.
        /// </summary>
        protected override int _iterationCount => 1;

        /// <summary>
        /// Serialization json text <see cref="ComparativeTestObjects.TestObject"/> benchmark.
        /// </summary>
        [Benchmark]
        public void JsonSerializationBenchmark()
        {
            this.RunInIteration(() =>
            {
                JsonConvert.SerializeObject(ComparativeTestObjects.TestObject);
            });
        }

        /// <summary>
        /// Serialization <see cref="BinaryFormatter"/> test <see cref="ComparativeTestObjects.TestObject"/> benchmark.
        /// </summary>
        [Benchmark]
        public void BinarySerializationBenchmark()
        {
            this.RunInIteration(() =>
            {
                BinaryFormatter.Serialize(ComparativeTestObjects.TestObject);
            });
        }

        /// <summary>
        /// Serialization <see cref="MessagePack"/> test <see cref="ComparativeTestObjects.TestObject"/> benchmark.
        /// </summary>
        [Benchmark]
        public void MessagePackSerializationBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ComparativeTestObjects.TestObject);
            });
        }
    }
}
