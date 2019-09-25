using System;

namespace Calabonga.AspNetCore.Micro.Core.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
    public class MicroserviceUnauthorizedException : Exception
    {
        public MicroserviceUnauthorizedException() : base(AppData.Exceptions.UnauthorizedException)
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
