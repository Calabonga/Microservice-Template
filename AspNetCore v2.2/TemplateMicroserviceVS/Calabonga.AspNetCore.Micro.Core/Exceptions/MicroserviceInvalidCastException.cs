using System;

namespace $safeprojectname$.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
    public class MicroserviceInvalidCastException : Exception
    {
        public MicroserviceInvalidCastException() : base(AppData.Exceptions.TypeConverterException)
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
