using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent NotFound Exception
    /// </summary>
    [Serializable]
    public class MicroserviceNotFoundException : Exception
    {
        public MicroserviceNotFoundException() : base(AppContracts.Exceptions.NotFoundException)
        {

        }

        public MicroserviceNotFoundException(string message) : base(message)
        {

        }
        
        public MicroserviceNotFoundException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
