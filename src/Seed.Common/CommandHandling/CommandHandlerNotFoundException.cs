using System;
using System.Runtime.Serialization;

namespace Seed.Common.CommandHandling
{
    [Serializable]
    public class CommandHandlerNotFoundException : CommandBusException
    {
        private const string MessageFormat = @"Handler for command of type ""{0}"" not found.";

        public CommandHandlerNotFoundException(Type commandType)
            : base(String.Format(MessageFormat, commandType.FullName))
        {
        }

        protected CommandHandlerNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}