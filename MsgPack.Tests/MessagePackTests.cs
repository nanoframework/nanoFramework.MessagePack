using nanoFramework.MessagePack;
using System.Collections;

namespace MsgPack.Tests
{
    //[TestClass]
    //public sealed class MessagePackTests
    //{
    //    [TestMethod]
    //    public void TestConverterContext()
    //    {
    //        var converter = ConverterContext.GetConverter(typeof(bool));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(short));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(ushort));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(int));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(uint));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(long));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(ulong));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(byte));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(sbyte));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(float));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(double));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(string));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(TimeSpan));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(DateTime));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(char));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(Guid));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(byte[]));
    //        Assert.IsNotNull(converter);
    //        converter = ConverterContext.GetConverter(typeof(ArrayList));
    //        Assert.IsNotNull(converter);

    //        Assert.IsNotNull(ConverterContext.NullConverter);
    //    }

    //    [TestMethod]
    //    public void SerializeTest()
    //    {
    //        Hashtable ht = [];
    //        ht.Add("TestId", "TestValue");
    //        var test = new TestClass()
    //        {
    //            Id = 1,
    //            Name = "Первый тестовый класс",
    //            FloatArray = [1.0f, 345675.31f],
    //            StringArray = ["Test1", "test2"],
    //            LongTwoDimensionalArray = [[1274753, 217374, 1923939393], [1,2,34]],
    //            SubTestObject = new()
    //            {
    //                TestHashtable = ht,
    //                Bytes = [1, 2, 46, 128],
    //                Ints = [35464, 3657585, 292939293],
    //                DoubleArray = [123.25364, 27484.484858],
    //                GuidArray = [Guid.NewGuid(), Guid.NewGuid()]
    //            },
    //            SubTestObjectArray = new SubTestClass[] { new SubTestClass() { TestHashtable = ht } }
    //        };

    //        test.TestArrayList.Add(1L);
    //        test.TestArrayList.Add(new TestClass() { Name = "ArrayList test" });

    //        var result = MessagePackSerializer.Serialize(test);
    //        var testResult = (TestClass) MessagePackSerializer.Deserialize(typeof(TestClass), result)!;
    //    }
    //}

    //public class TestClass
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //    public float[] FloatArray { get; set; }

    //    public long[][] LongTwoDimensionalArray { get; set; }

    //    public string[] StringArray { get; set; }

    //    public SubTestClass SubTestObject { get; set; }

    //    public SubTestClass[] SubTestObjectArray { get; set; }

    //    public ArrayList TestArrayList { get; set; } = new();
    //}

    //public class SubTestClass
    //{
    //    public Hashtable TestHashtable { get; set; }

    //    public byte[] Bytes { get; set; }

    //    public int[] Ints { get; set; }

    //    public double[] DoubleArray { get; set; }

    //    public Guid[] GuidArray { get; set; }
    //}
}
