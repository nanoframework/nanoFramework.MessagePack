// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using nanoFramework.Benchmark.Attributes;
using nanoFramework.Benchmark;
using nanoFramework.MessagePack.Benchmark.Base;
using System;

namespace nanoFramework.MessagePack.Benchmark.DeserializationBenchmarks
{
    [IterationCount(5)]
    public class ValueTypesDeserializationBenchmark : BaseIterationBenchmark
    {
        protected override int IterationCount => 20;
        private readonly byte[] ShortBytes = MessagePackSerializer.Serialize(short.MaxValue);
        private readonly byte[] UshortBytes = MessagePackSerializer.Serialize(ushort.MaxValue);
        private readonly byte[] IntBytes = MessagePackSerializer.Serialize(int.MaxValue);
        private readonly byte[] UintBytes = MessagePackSerializer.Serialize(uint.MaxValue);
        private readonly byte[] LongBytes = MessagePackSerializer.Serialize(long.MaxValue);
        private readonly byte[] UlongBytes = MessagePackSerializer.Serialize(ulong.MaxValue);
        private readonly byte[] ByteBytes = MessagePackSerializer.Serialize(byte.MaxValue);
        private readonly byte[] SbyteBytes = MessagePackSerializer.Serialize(sbyte.MaxValue);
        private readonly byte[] FloatBytes = MessagePackSerializer.Serialize(float.MaxValue);
        private readonly byte[] DoubleBytes = MessagePackSerializer.Serialize(double.MaxValue);
        private readonly byte[] BoolBytes = MessagePackSerializer.Serialize(true);
        private readonly byte[] CharBytes = MessagePackSerializer.Serialize(char.MaxValue);
        private readonly byte[] StringBytes = MessagePackSerializer.Serialize("Benchmark test");
        private readonly byte[] DateTimeBytes = MessagePackSerializer.Serialize(DateTime.UtcNow);
        private readonly byte[] TimeSpanBytes = MessagePackSerializer.Serialize(TimeSpan.MaxValue);
        private readonly byte[] GuidBytes = MessagePackSerializer.Serialize(Guid.NewGuid());

        [Benchmark]
        public void GuidBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(Guid), GuidBytes);
            });
        }

        [Benchmark]
        public void ShortBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(short), ShortBytes);
            });
        }

        [Benchmark]
        public void UshortBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(ushort), UshortBytes);
            });
        }

        [Benchmark]
        public void IntBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(int), IntBytes);
            });
        }

        [Benchmark]
        public void UintBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(uint), UintBytes);
            });
        }

        [Benchmark]
        public void SbyteBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(sbyte), SbyteBytes);
            });
        }

        [Benchmark]
        public void ByteBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(byte), ByteBytes);
            });
        }

        [Benchmark]
        public void BoolBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(bool), BoolBytes);
            });
        }

        [Benchmark]
        public void CharBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(char), CharBytes);
            });
        }

        [Benchmark]
        public void StringBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(string), StringBytes);
            });
        }

        [Benchmark]
        public void TimeSpanBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(TimeSpan), TimeSpanBytes);
            });
        }

        [Benchmark]
        public void FloatBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(float), FloatBytes);
            });
        }

        [Benchmark]
        public void DoubleBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(double), DoubleBytes);
            });
        }

        [Benchmark]
        public void DateTimeBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(DateTime), DateTimeBytes);
            });
        }

        [Benchmark]
        public void LongBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(long), LongBytes);
            });
        }

        [Benchmark]
        public void UlongBenchmark()
        {
            RunInIteration(() =>
            {
                MessagePackSerializer.Deserialize(typeof(ulong), UlongBytes);
            });
        }
    }
}
