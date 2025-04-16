// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace nanoFramework.MessagePack.Exceptions
{
    /// <summary>
    /// Exception for serialization errors.
    /// </summary>
    public class SerializationException : Exception
    {
        /// <summary>
        /// Serialization exception.
        /// </summary>
        /// <param name="message"></param>
        public SerializationException(string message) : base(message)
        {

        }
    }
}
