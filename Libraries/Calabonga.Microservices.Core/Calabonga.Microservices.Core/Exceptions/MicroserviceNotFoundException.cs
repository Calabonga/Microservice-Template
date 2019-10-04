using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
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
