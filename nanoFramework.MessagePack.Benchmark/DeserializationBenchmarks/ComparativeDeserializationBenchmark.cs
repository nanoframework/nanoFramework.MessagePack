// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.MessagePack.Benchmark.DeserializationBenchmarks
{
    using System.Runtime.Serialization.Formatters.Binary;
    using nanoFramework.Benchmark;
    using nanoFramework.Benchmark.Attributes;
    using nanoFramework.Json;
    using nanoFramework.MessagePack.Benchmark.Base;
    using nanoFramework.MessagePack.Benchmark.Data;
    using static nanoFramework.MessagePack.Benchmark.Data.ComparativeTestObjects;

    /// <summary>
    /// Comparative deserialization benchmark.
    /// </summary>
    [IterationCount(10)]
    public class ComparativeDeserializationBenchmark : BaseIterationBenchmark
    {
        /// <summary>
        /// public iteration count.
        /// </summary>
        protected override int _iterationCount => 1;

        /// <summary>
        /// Deserialization json benchmark.
        /// </summary>
        [Benchmark]
        public void JsonDeserializationBenchmark()
        {
            this.RunInIteration(() =>
            {
                JsonConvert.DeserializeObject(TestJson, typeof(TestObjectClass), ComparativeTestObjects.JsonSerializerOptions);
            });
        }

        /// <summary>
        /// Deserialization <see cref="BinaryFormatter"/> binary array benchmark.
        /// </summary>
        [Benchmark]
        public void BinaryDeserializationBenchmark()
        {
            this.RunInIteration(() =>
            {
                BinaryFormatter.Deserialize(TestObjectBinaryBytes);
            });
        }

        /// <summary>
        /// Deserialization <see cref="MessagePack"/> binary array benchmark.
        /// </summary>
        [Benchmark]
        public void MessagePackDeserializationBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(TestObjectClass), TestObjectMsgPackBytes);
            });
        }
    }
}
