using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent Unauthorized Exception
    /// </summary>
    [Serializable]
    public class MicroserviceUnauthorizedException : Exception
    {
        public MicroserviceUnauthorizedException() : base(AppContracts.Exceptions.UnauthorizedException)
        {

        }

        public MicroserviceUnauthorizedException(string message) : base(message)
        {

        }
        
        public MicroserviceUnauthorizedException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
