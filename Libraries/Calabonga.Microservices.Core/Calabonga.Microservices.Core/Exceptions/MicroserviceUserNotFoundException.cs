using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
    [Serializable]
    public class MicroserviceUserNotFoundException : Exception
    {
        public MicroserviceUserNotFoundException() : base(AppContracts.Exceptions.UserNotFoundException)
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
