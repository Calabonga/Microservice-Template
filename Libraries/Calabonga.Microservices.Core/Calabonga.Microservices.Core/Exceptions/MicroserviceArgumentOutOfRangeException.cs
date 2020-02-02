﻿using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent Price Point Exception
    /// </summary>
    [Serializable]
    public class MicroserviceArgumentOutOfRangeException : Exception
    {
        public MicroserviceArgumentOutOfRangeException() : base(AppContracts.Exceptions.ArgumentOutOfRangeException)
        {

        }

        public MicroserviceArgumentOutOfRangeException(string message) : base(message)
        {

        }

        public MicroserviceArgumentOutOfRangeException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
