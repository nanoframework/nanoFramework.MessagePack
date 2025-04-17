// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;

namespace UnitTestShared.TestData
{
    public static class SharedWordDictionary
    {
        static SharedWordDictionary()
        {
            WordDictionary = new()
            {
                "MessagePak",
                "Hello",
                "at",
                "nanoFramework!",
                " "
            };
        }

        public static ArrayList WordDictionary { get; }
    }
}
