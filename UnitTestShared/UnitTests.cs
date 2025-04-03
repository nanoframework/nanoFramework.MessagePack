using nanoFramework.MessagePack;
using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using UnitTestShared.Helpers;
using UnitTestShared.TestData;
#if NANOFRAMEWORK_1_0
using nanoFramework.TestFramework;
#endif

namespace NFUnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestConverterContext()
        {
            var converter = ConverterContext.GetConverter(typeof(bool));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(short));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(ushort));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(int));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(uint));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(long));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(ulong));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(byte));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(sbyte));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(float));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(double));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(string));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(TimeSpan));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(DateTime));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(char));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(Guid));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(byte[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(ArrayList));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(int[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(uint[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(long[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(ulong[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(float[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(double[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(char[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(string[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(Guid[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(sbyte[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(DateTime[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(TimeSpan[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(bool[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(short[]));
            Assert.IsNotNull(converter);
            converter = ConverterContext.GetConverter(typeof(ushort[]));
            Assert.IsNotNull(converter);

            Assert.IsNotNull(ConverterContext.NullConverter);

            var testConverter = TestsHelper.GetTestConverter();
            ConverterContext.Add(typeof(byte[][]), testConverter);
            converter = ConverterContext.GetConverter(typeof(byte[][]));
            Assert.IsNotNull(converter);
            ConverterContext.Replace(typeof(byte[][]), testConverter);
            converter = ConverterContext.GetConverter(typeof(byte[][]));
            Assert.IsNotNull(converter);
            ConverterContext.Remove(typeof(byte[][]));
            converter = ConverterContext.GetConverter(typeof(byte[][]));
            Assert.IsNull(converter);
#if NANOFRAMEWORK_1_0
            Assert.ThrowsException(typeof(NotSupportedException), () => ConverterContext.Add(typeof(object), testConverter));
#else
            Assert.ThrowsException<NotSupportedException>(() => ConverterContext.Add(typeof(object), testConverter));
#endif
        }

        [TestMethod]
        public void ProcessCustomObjectTest()
        {
            var test = TestsHelper.GetTestClassObject();
            var result = MessagePackSerializer.Serialize(test);
            Debug.WriteLine($"Serialize byte size: {result.Length}");
            var testResult = (TestClass)MessagePackSerializer.Deserialize(typeof(TestClass), result)!;
            Assert.IsNotNull(testResult);

            Assert.AreEqual(test, testResult);
        }
    }

    
}
