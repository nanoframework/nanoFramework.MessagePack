﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using System;
using System.Collections;
using System.Diagnostics;
using nanoFramework.MessagePack;
using UnitTestShared.Helpers;
using UnitTestShared.TestData;
using System.IO;
#if NANOFRAMEWORK_1_0
using nanoFramework.TestFramework;
using nanoFramework.Json;
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
            var resultBytes = MessagePackSerializer.Serialize(test);
            Debug.WriteLine($"Serialize byte size: {resultBytes.Length}");
            var testResult = (TestClass)MessagePackSerializer.Deserialize(typeof(TestClass), resultBytes)!;
            Assert.IsNotNull(testResult);
#if NANOFRAMEWORK_1_0
            var expected = JsonConvert.SerializeObject(test);
            var actual = JsonConvert.SerializeObject(testResult);
            Assert.AreEqual(expected, actual);        
#endif
            Assert.AreEqual(test, testResult);

            using MemoryStream ms = new(resultBytes);

            var msTestResult = (TestClass)MessagePackSerializer.Deserialize(typeof(TestClass), ms)!;

#if NANOFRAMEWORK_1_0
            expected = JsonConvert.SerializeObject(testResult);
            actual = JsonConvert.SerializeObject(msTestResult);
            Assert.AreEqual(expected, actual);        
#endif
            Assert.AreEqual(testResult, msTestResult);
        }

        [TestMethod]
        public void CustomConverterTest()
        {
            var secureMessageConverter = new SecureMessageConverter();
            ConverterContext.Add(typeof(SecureMessage), secureMessageConverter);

            var secureMessage = new SecureMessage("Hello MessagePack at nanoFramework!");

            //At sender
            var buffer = MessagePackSerializer.Serialize(secureMessage);
            Debug.WriteLine($"The message:\n{secureMessage.Message}\nbeing sent has been serialized into {buffer.Length} bytes.");
            //and sent to recipient
            //
            //..........................
            Debug.WriteLine("=============================================");

            //At recipient, after receiving the byte array
            Debug.WriteLine($"Received {buffer.Length} bytes");

            var recipientSecureMessage = (SecureMessage)MessagePackSerializer.Deserialize(typeof(SecureMessage), buffer)!;

            Debug.WriteLine($"Message received:\n{recipientSecureMessage.Message}");

            Assert.AreNotEqual(secureMessage, recipientSecureMessage);

            Assert.AreEqual(secureMessage.Message, recipientSecureMessage.Message);
        }
    }


}
