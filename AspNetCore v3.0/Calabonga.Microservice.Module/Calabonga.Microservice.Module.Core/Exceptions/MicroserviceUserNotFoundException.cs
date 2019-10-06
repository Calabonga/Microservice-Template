using System;

namespace Calabonga.Microservice.Module.Core.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
    public class MicroserviceUserNotFoundException : Exception
    {
        public MicroserviceUserNotFoundException() : base(AppData.Exceptions.UserNotFoundException)
        {

        }

        public MicroserviceUserNotFoundException(string message) : base(message)
        {
        }

        public MicroserviceUserNotFoundException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
