namespace $safeprojectname$.Core
{
    public static partial class AppData
    {
        /// <summary>
        /// Exception messages
        /// </summary>
        public static class Exceptions
        {
            /// <summary>
            /// "An exception was thrown"
            /// </summary>
            public static string ThrownException => "An exception was thrown";

            /// <summary>
            /// "Invalid cast exception"
            /// </summary>
            public static string TypeConverterException => "Invalid cast exception";

            /// <summary>
            /// "User not registered in the system"
            /// </summary>
            public static string UserNotFoundException => "User not registered in the system";

            /// <summary>
            /// "Object not found"
            /// </summary>
            public static string NotFoundException => "Object not found";

            /// <summary>
            /// "Unauthorized access denied"
            /// </summary>
            public static string UnauthorizedException => "Unauthorized access denied";

            /// <summary>
            /// "Argument null exception"
            /// </summary>
            public static string ArgumentNullException => "Argument null exception";

            /// <summary>
            /// "File already exists"
            /// </summary>
            public static string FileAlreadyExists => "File already exists";

            /// <summary>
            /// "Email Service Unavailable or SMTP provider error occupied"
            /// </summary>
            public static string EmailServiceUnavailable => "Email Service Unavailable or SMTP provider error occupied";

            /// <summary>
            /// "Some errors occurred while checking the entity"
            /// </summary>
            public static string EntityValidationException => "Some errors occurred while checking the entity";

            /// <summary>
            /// "Invalid operation exception was thrown"
            /// </summary>
            public static string InvalidOperationException => "Invalid operation exception was thrown";
        }
    }
}
