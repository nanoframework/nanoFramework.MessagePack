// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.Benchmark;
using nanoFramework.MessagePack.Benchmark.Base;

namespace nanoFramework.MessagePack.Benchmark.SerializationBenchmarks
{
    [IterationCount(5)]
    public class ValueTypesSerializationBenchmark : BaseIterationBenchmark
    {
        protected override int IterationCount => 20;

        private readonly Guid TestGuid = Guid.NewGuid();
        private readonly DateTime TestDateTime = DateTime.UtcNow;
        private readonly string TestString = "Benchmark test";

        [Benchmark]
        public void GuidBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(TestGuid);
            });
        }

        [Benchmark]
        public void ShortBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(short.MaxValue);
            });
        }

        [Benchmark]
        public void UshortBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ushort.MaxValue);
            });
        }

        [Benchmark]
        public void IntBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(int.MaxValue);
            });
        }

        [Benchmark]
        public void UintBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(uint.MaxValue);
            });
        }

        [Benchmark]
        public void SbyteBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(sbyte.MaxValue);
            });
        }

        [Benchmark]
        public void ByteBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(byte.MaxValue);
            });
        }

        [Benchmark]
        public void BoolBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(true);
            });
        }

        [Benchmark]
        public void CharBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(char.MaxValue);
            });
        }

        [Benchmark]
        public void StringBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(TestString);
            });
        }

        [Benchmark]
        public void TimeSpanBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(TimeSpan.MaxValue);
            });
        }

        [Benchmark]
        public void FloatBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(float.MaxValue);
            });
        }

        [Benchmark]
        public void DoubleBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(double.MaxValue);
            });
        }

        [Benchmark]
        public void DateTimeBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(TestDateTime);
            });
        }

        [Benchmark]
        public void LongBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(long.MaxValue);
            });
        }

        [Benchmark]
        public void UlongBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Serialize(ulong.MaxValue);
            });
        }
    }
}
