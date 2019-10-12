using System.Runtime.Serialization;

namespace Calabonga.Microservices.Core
{
    /// <summary>
    /// Architecture infrastructure common symbols
    /// </summary>
    public static partial class AppContracts
    {
        public static class Exceptions
        {
           
            /// <summary>
            /// "User not registered in the system"
            /// </summary>
            public static string UserNotFoundException => "User not registered in the system";
            
            /// <summary>
            /// "Some errors occurred while checking the entity"
            /// </summary>
            public static string EntityValidationException => "Some errors occurred while checking the entity";

            /// <summary>
            /// "Unauthorized access denied"
            /// </summary>
            public static string UnauthorizedException => "Unauthorized access denied";

            /// <summary>
            /// "Object not found"
            /// </summary>
            public static string NotFoundException => "Object not found";

            
            /// <summary>
            /// "Argument null exception"
            /// </summary>
            public static string ArgumentNullException => "Argument null exception";
            
            /// <summary>
            /// "Invalid operation exception: {0}"
            /// </summary>
            public static string InvalidOperationException => "Invalid operation exception: {0}";

            /// <summary>
            /// "Argument Out Of Range Exception"
            /// </summary>
            public static string ArgumentOutOfRangeException => "Argument Out Of Range Exception";

            /// <summary>
            /// "Microservice Thrown Exception"
            /// </summary>
            public static string ThrownException => "Microservice Thrown Exception";

            /// <summary>
            /// "File Already Exists";
            /// </summary>
            public static string FileAlreadyExists => "File Already Exists";

            /// <summary>
            /// "Type Converter Exception"
            /// </summary>
            public static string TypeConverterException => "Type Converter Exception";

            /// <summary>
            /// "AutoMapper Mapping exception";
            /// </summary>
            public static string MappingException => "AutoMapper Mapping exception";
        }
    }
}
