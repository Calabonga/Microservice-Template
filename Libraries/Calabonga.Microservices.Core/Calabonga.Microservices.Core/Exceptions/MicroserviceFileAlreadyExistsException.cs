using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// FileAlreadyExists 
    /// </summary>
    public class MicroserviceFileAlreadyExistsException: Exception
    {
        public MicroserviceFileAlreadyExistsException() : base(AppContracts.Exceptions.FileAlreadyExists)
        {

        }

        public MicroserviceFileAlreadyExistsException(string message) : base(message)
        {

        }

        public MicroserviceFileAlreadyExistsException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
