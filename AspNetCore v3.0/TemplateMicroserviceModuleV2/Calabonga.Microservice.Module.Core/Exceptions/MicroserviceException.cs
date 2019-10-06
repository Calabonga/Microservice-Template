using System;

namespace $safeprojectname$.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
    public class MicroserviceException : Exception
    {
        public MicroserviceException() : base(AppData.Exceptions.ThrownException)
        {

        }

        public MicroserviceException(string message) : base(message)
        {

        }

        public MicroserviceException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
