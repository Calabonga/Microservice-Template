using System;

namespace Calabonga.Microservice.IdentityModule.Core.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
    public class MicroserviceArgumentOutOfRangeException : Exception
    {
        public MicroserviceArgumentOutOfRangeException() : base(AppData.Exceptions.ArgumentOutOfRangeException)
        {

        }

        public MicroserviceArgumentOutOfRangeException(string message) : base(message)
        {

        }

        public MicroserviceArgumentOutOfRangeException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
