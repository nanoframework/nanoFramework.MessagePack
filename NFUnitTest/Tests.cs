

using nanoFramework.MessagePack;
using nanoFramework.TestFramework;
using System;
using UnitTestShared.TestData;

namespace NFUnitTest
{
    [TestClass]
    internal class Tests
    {
        [TestMethod]
        public void TestMethod()
        {
            Assert.ThrowsException(typeof(NotSupportedException), () => ConverterContext.Add(typeof(object), new TestConverter()));
        }
    }
}
