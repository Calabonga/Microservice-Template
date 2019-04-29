using System;

namespace $safeprojectname$.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
    public class MicroserviceArgumentNullException : Exception
    {
        public MicroserviceArgumentNullException() : base(AppData.Exceptions.ArgumentNullException)
        {

        }

        public MicroserviceArgumentNullException(string message) : base(message)
        {

        }

        public MicroserviceArgumentNullException(string message, Exception exception) : base(message, exception)
        {

        }

        public MicroserviceArgumentNullException(Exception exception) : base(AppData.Exceptions.ArgumentNullException, exception)
        {

        }
    }
}
