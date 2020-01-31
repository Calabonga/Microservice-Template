using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent Invalid casting Exception
    /// </summary>
    [Serializable]
    public class MicroserviceInvalidCastException : Exception
    {
        public MicroserviceInvalidCastException() : base(AppContracts.Exceptions.TypeConverterException)
        {

        }

        public MicroserviceInvalidCastException(string message) : base(message)
        {

        }
        
        public MicroserviceInvalidCastException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
