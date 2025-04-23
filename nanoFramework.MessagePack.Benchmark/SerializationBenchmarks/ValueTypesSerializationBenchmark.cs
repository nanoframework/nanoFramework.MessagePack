// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.MessagePack.Benchmark.SerializationBenchmarks
{
    using System;
    using nanoFramework.Benchmark;
    using nanoFramework.Benchmark.Attributes;
    using nanoFramework.MessagePack.Benchmark.Base;

    /// <summary>
    /// Value types serialization benchmark
    /// </summary>
    [IterationCount(5)]
    public class ValueTypesSerializationBenchmark : BaseIterationBenchmark
    {
        /// <summary>
        /// Test <see cref="Guid"/>
        /// </summary>
        private readonly Guid testGuid = Guid.NewGuid();

        /// <summary>
        /// Test <see cref="DateTime"/>
        /// </summary>
        private readonly DateTime testDateTime = DateTime.UtcNow;

        /// <summary>
        /// Test <see cref="string"/>
        /// </summary>
        private readonly string testString = "Benchmark test";

        /// <summary>
        /// public iteration count.
        /// </summary>
        protected override int _iterationCount => 20;

        /// <summary>
        /// Serialization <see cref="Guid"/> benchmark.
        /// </summary>
        [Benchmark]
        public void GuidBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(testGuid);
            });
        }

        /// <summary>
        /// Serialization <see cref="short"/> benchmark.
        /// </summary>
        [Benchmark]
        public void ShortBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(short.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="ushort"/> benchmark.
        /// </summary>
        [Benchmark]
        public void UshortBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ushort.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="int"/> benchmark.
        /// </summary>
        [Benchmark]
        public void IntBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(int.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="uint"/> benchmark.
        /// </summary>
        [Benchmark]
        public void UintBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(uint.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="sbyte"/> benchmark.
        /// </summary>
        [Benchmark]
        public void SbyteBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(sbyte.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="byte"/> benchmark.
        /// </summary>
        [Benchmark]
        public void ByteBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(byte.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="bool"/> benchmark.
        /// </summary>
        [Benchmark]
        public void BoolBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(true);
            });
        }

        /// <summary>
        /// Serialization <see cref="char"/> benchmark.
        /// </summary>
        [Benchmark]
        public void CharBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(char.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="string"/> benchmark.
        /// </summary>
        [Benchmark]
        public void StringBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(testString);
            });
        }

        /// <summary>
        /// Serialization <see cref="TimeSpan"/> benchmark.
        /// </summary>
        [Benchmark]
        public void TimeSpanBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(TimeSpan.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="float"/> benchmark.
        /// </summary>
        [Benchmark]
        public void FloatBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(float.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="double"/> benchmark.
        /// </summary>
        [Benchmark]
        public void DoubleBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(double.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="DateTime"/> benchmark.
        /// </summary>
        [Benchmark]
        public void DateTimeBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(testDateTime);
            });
        }

        /// <summary>
        /// Serialization <see cref="long"/> benchmark.
        /// </summary>
        [Benchmark]
        public void LongBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(long.MaxValue);
            });
        }

        /// <summary>
        /// Serialization <see cref="ulong"/> benchmark.
        /// </summary>
        [Benchmark]
        public void UlongBenchmark()
        {
            this.RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ulong.MaxValue);
            });
        }
    }
}
