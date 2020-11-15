using System;

namespace Calabonga.Microservice.Module.Core.Exceptions
{
    /// <summary>
    /// FileAlreadyExists 
    /// </summary>
    public class MicroserviceFileAlreadyExistsException : Exception
    {
        public MicroserviceFileAlreadyExistsException() : base(AppData.Exceptions.FileAlreadyExists)
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
