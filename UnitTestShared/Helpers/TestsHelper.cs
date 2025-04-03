
using System;
using System.Collections;
using UnitTestShared.TestData;

namespace UnitTestShared.Helpers
{
    internal static class TestsHelper
    {
        internal static TestClass GetTestClassObject()
        {
            Hashtable ht = new();
            ht.Add("TestId", "TestValue");
            var test = new TestClass()
            {
                Id = 1,
                Name = "Firs test object",
                FloatArray = new float[] { 1.0f, 345675.31f },
                StringArray = new string[] { "Test1", "test2" },
                LongTwoDimensionalArray = new long[][]
                { new long[] {1274753, 217374, 1923939393 }, new long[] {1, 2, 34 } },
                SubTestObject = new()
                {
                    TestHashtable = ht,
                    Bytes = new byte[] { 1, 2, 46, 128 },
                    Ints = new int[] { 35464, 3657585, 292939293 },
                    DoubleArray = new double[] { 123.25364, 27484.484858 },
                    GuidArray = new Guid[] { Guid.NewGuid(), Guid.NewGuid() }
                },
                SubTestObjectArray = new TestSubClass[] { new TestSubClass() { TestHashtable = ht } },
                CharArray = new char[]
                {
                    char.MinValue,
                    '\u0001',
                    '\u0002',
                    '\u0003',
                    '\u0004',
                    '\u0005',
                    '\u0006',
                    '\u0007',
                    '\u0008',
                    '\u0009',
                    '\u0010',
                    '\u0011',
                    '\u0012',
                    '\u0013',
                    '\u0014',
                    '\u0015'
                },
                fieldTest = 5
            };

            test.TestArrayList.Add(1L);
            //test.TestArrayList.Add(new TestClass() { Name = "ArrayList test" });

            return test;
        }

        internal static TestConverter GetTestConverter()
        {
            return new TestConverter();
        }
#nullable enable
        internal static bool ArrayEqual(this IList? array1, IList? array2)
        {
            if (array1 == null && array2 == array1)
                return true;
            if(array1!.Count == array2!.Count)
            {
                if (array1.Count > 0)
                {
                    if (array1[0] is IDictionary)
                    {
                        for (int i = 0; i < array1.Count; i++)
                        {
                            if (!((IDictionary)array1[i]!).DictionaryEqual((IDictionary)array2[i]!))
                                return false;
                        }
                        
                    }
                    else if (!array1[0]!.GetType().IsArray)
                    {
                        for (int i = 0; i < array1.Count; i++)
                        {
                            if (!array1[i]!.Equals(array2[i]))
                                return array1[i]!.ToString() == array2[i]!.ToString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < array1.Count; i++)
                        {
                            if (!((IList)array1[i]!).ArrayEqual((IList)array2[i]!))
                                return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        internal static bool DictionaryEqual(this IDictionary? array1, IDictionary? array2)
        {
            if (array1 == null && array2 == array1)
                return true;
            if (array1!.Count == array2!.Count)
            {
                if (array1.Count > 0)
                {
                    for (int i = 0; i < array1.Count; i++)
                    {
                        if (array1[i] != array2[i] && array1[i] != null)
                        {
                            if (array1[i] is IDictionary dictionary)
                            {
                                if (!dictionary.DictionaryEqual((IDictionary)array2[i]!))
                                    return false;
                            }
                            else if (!array1[i]!.GetType().IsArray)
                            {

                                if (!array1[i]!.Equals(array2[i]))
                                    return false;
                            }
                            else
                            {
                                if (!((IList)array1[i]!).ArrayEqual((IList)array2[i]!))
                                    return false;
                            }
                        }
                    }
                }

                return true;
            }

            return false;
        }
    }
}
