using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent ArgumentNull Exception
    /// </summary>
    [Serializable]
    public class MicroserviceArgumentNullException : Exception
    {
        public MicroserviceArgumentNullException() : base(AppContracts.Exceptions.ArgumentNullException)
        {

        }

        public MicroserviceArgumentNullException(string message) : base(message)
        {

        }

        public MicroserviceArgumentNullException(string message, Exception exception) : base(message, exception)
        {

        }

        public MicroserviceArgumentNullException(Exception exception) : base(AppContracts.Exceptions.ArgumentNullException, exception)
        {

        }
    }
}
