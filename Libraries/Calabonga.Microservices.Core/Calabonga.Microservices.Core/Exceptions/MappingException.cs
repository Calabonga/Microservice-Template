using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Mapping exception
    /// </summary>
    [Serializable]
    public class MappingException : Exception
    {
        public MappingException() : base(AppContracts.Exceptions.MappingException)
        {

        }

        public MappingException(string message) : base(message)
        {

        }
        
        public MappingException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}