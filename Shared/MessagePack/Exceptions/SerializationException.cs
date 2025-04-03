using System;

namespace nanoFramework.MessagePack.Exceptions
{
    public class SerializationException : Exception
    {
        internal SerializationException(string message) : base(message)
        {

        }
    }
}
