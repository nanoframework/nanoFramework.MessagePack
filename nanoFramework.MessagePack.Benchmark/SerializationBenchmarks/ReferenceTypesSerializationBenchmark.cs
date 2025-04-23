// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.MessagePack.Benchmark.SerializationBenchmarks
{
    using nanoFramework.Benchmark;
    using nanoFramework.Benchmark.Attributes;
    using nanoFramework.MessagePack.Benchmark.Base;
    using nanoFramework.MessagePack.Benchmark.Data;

    /// <summary>
    /// Reference types serialization benchmark.
    /// </summary>
    [IterationCount(5)]
    public class ReferenceTypesSerializationBenchmark : BaseIterationBenchmark
    {
        /// <summary>
        /// public iteration count.
        /// </summary>
        protected override int _iterationCount => 20;

        /// <summary>
        /// Serialization <see cref="ReferenceTestObjects.IntArray"/> benchmark.
        /// </summary>
        [Benchmark]
        public void IntArrayBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ReferenceTestObjects.IntArray);
            });
        }

        /// <summary>
        /// Serialization <see cref="ReferenceTestObjects.ByteArray"/> benchmark.
        /// </summary>
        [Benchmark]
        public void ByteArrayBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ReferenceTestObjects.ByteArray);
            });
        }

        /// <summary>
        /// Serialization <see cref="ReferenceTestObjects.TwoDimensionalArray"/> benchmark.
        /// </summary>
        [Benchmark]
        public void TwoDimensionalArrayBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ReferenceTestObjects.TwoDimensionalArray);
            });
        }

        /// <summary>
        /// Serialization <see cref="ReferenceTestObjects.TestHashtable"/> benchmark.
        /// </summary>
        [Benchmark]
        public void TestHashtableBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ReferenceTestObjects.TestHashtable);
            });
        }
    }
}
