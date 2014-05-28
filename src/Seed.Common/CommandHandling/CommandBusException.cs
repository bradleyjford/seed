using System;
using System.Runtime.Serialization;

namespace Seed.Common.CommandHandling
{
    [Serializable]
    public abstract class CommandBusException : Exception
    {
        protected CommandBusException(string message) 
            : base(message)
        {
        }

        protected CommandBusException(string message, Exception inner) 
            : base(message, inner)
        {
        }

        protected CommandBusException(
            SerializationInfo info,
            StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
