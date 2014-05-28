using System;
using System.Runtime.Serialization;

namespace Seed.Common.CommandHandling
{
    [Serializable]
    public class CommandValidatorNotFoundException : CommandBusException
    {
        private const string MessageFormat = @"Validator for command of type ""{0}"" not found.";

        public CommandValidatorNotFoundException(Type commandType)
            : base(String.Format(MessageFormat, commandType.FullName))
        {
        }

        protected CommandValidatorNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}