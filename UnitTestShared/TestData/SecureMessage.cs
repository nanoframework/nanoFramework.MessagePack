// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace UnitTestShared.TestData
{
    public class SecureMessage
    {
        public SecureMessage(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
