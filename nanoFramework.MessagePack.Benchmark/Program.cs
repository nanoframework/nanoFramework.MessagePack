// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics;
using System.Threading;
using nanoFramework.Benchmark;
using nanoFramework.Json;
using nanoFramework.MessagePack.Benchmark.Data;
using nanoFramework.MessagePack.Benchmark.DeserializationBenchmarks;
using nanoFramework.MessagePack.Benchmark.SerializationBenchmarks;

namespace nanoFramework.MessagePack.Benchmark
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("********** Starting benchmarks **********");
            BenchmarkRunner.RunClass(typeof(ReferenceTypesDeserializationBenchmark));
            BenchmarkRunner.RunClass(typeof(ValueTypesDeserializationBenchmark));
            BenchmarkRunner.RunClass(typeof(ReferenceTypesSerializationBenchmark));
            BenchmarkRunner.RunClass(typeof(ValueTypesSerializationBenchmark));

            //Init test objects
            var testData = new ComparativeTestObjects();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===============================================================");
            Console.WriteLine("==========         Comparative benchmarks data       ==========");
            Console.WriteLine("==========                                           ==========");
            Console.WriteLine($"========== Json string size:           {JsonConvert.SerializeObject(ComparativeTestObjects.TestObject).Length} bytes    ==========");
            Console.WriteLine($"========== BinaryFormatter array size: {ComparativeTestObjects.TestObjectBinaryBytes.Length} bytes    ==========");
            Console.WriteLine($"========== MessagePack array size:     {ComparativeTestObjects.TestObjectMsgPackBytes.Length} bytes    ==========");
            Console.WriteLine("==========                                           ==========");
            Console.WriteLine("===============================================================");

            BenchmarkRunner.RunClass(typeof(ComparativeDeserializationBenchmark));
            BenchmarkRunner.RunClass(typeof(ComparativeSerializationBenchmark));
            Console.WriteLine("********** Completed benchmarks **********");

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
