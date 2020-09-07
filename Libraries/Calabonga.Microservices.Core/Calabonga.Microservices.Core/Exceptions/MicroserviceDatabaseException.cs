
using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent Database Exception
    /// </summary>
    [Serializable]
    public class MicroserviceDatabaseException : Exception
    {
        public MicroserviceDatabaseException() : base(AppContracts.Exceptions.NotFoundException)
        {

        }

        public MicroserviceDatabaseException(string message) : base(message)
        {

        }
        
        public MicroserviceDatabaseException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
