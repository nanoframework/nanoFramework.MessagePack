// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace nanoFramework.MessagePack.Exceptions
{
    public class ConverterNotFoundException : Exception
    {
        public ConverterNotFoundException(Type type)
            : base($"Converter not found for type {type.FullName}")
        {
            ObjectType = type;
        }

        public Type ObjectType { get; }
    }
}
