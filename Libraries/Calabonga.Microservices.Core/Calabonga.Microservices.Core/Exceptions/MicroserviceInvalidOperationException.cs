using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent InvalidOperation Exception
    /// </summary>
    [Serializable]
    public class MicroserviceInvalidOperationException : Exception
    {
        public MicroserviceInvalidOperationException() : base(AppContracts.Exceptions.InvalidOperationException)
        {

        }

        public MicroserviceInvalidOperationException(string message) : base(message)
        {

        }
        
        public MicroserviceInvalidOperationException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
