using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent EntityValidation Exception
    /// </summary>
    [Serializable]
    public class MicroserviceEntityValidationException : Exception
    {
        public MicroserviceEntityValidationException() : base(AppContracts.Exceptions.EntityValidationException)
        {

        }

        public MicroserviceEntityValidationException(string message) : base(message)
        {

        }

        public MicroserviceEntityValidationException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
