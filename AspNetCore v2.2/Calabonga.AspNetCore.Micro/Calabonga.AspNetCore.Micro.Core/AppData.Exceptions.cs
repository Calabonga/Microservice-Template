namespace Calabonga.AspNetCore.Micro.Core
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
            /// "Hotel not found"
            /// </summary>
            public static string HotelNotFoundException => "Hotel not found";

            /// <summary>
            /// "Hotel identifier required"
            /// </summary>
            public static string HotelIdentifierRequiredException => "Hotel identifier required";

            /// <summary>
            /// "Unauthorized access denied"
            /// </summary>
            public static string UnauthorizedException => "Unauthorized access denied";

            /// <summary>
            /// "Argument null exception"
            /// </summary>
            public static string ArgumentNullException => "Argument null exception";

            /// <summary>
            /// "RoomType type not found"
            /// </summary>
            public static string RoomTypeNotFoundException => "RoomType type not found";

            /// <summary>
            /// "RoomType type not found"
            /// </summary>
            public static string RoomTypeNumberOfUnitsNotFoundException => "RoomType total of unit not defined";

            /// <summary>
            /// "Import type not recognized. Allowed formats .xlsx and .csv"
            /// </summary>
            public static string ImportTypeNotRecognized => "Import type not recognized. Allowed formats .xlsx and .csv";

            /// <summary>
            /// "Imported file name is invalid"
            /// </summary>
            public static string ImportFileNameInvalid => "Imported file name is invalid";

            /// <summary>
            /// "File already exists"
            /// </summary>
            public static string FileAlreadyExists => "File already exists";

            /// <summary>
            /// "Email Service Unavailable or SMTP provider error occupied"
            /// </summary>
            public static string EmailServiceUnavailable => "Email Service Unavailable or SMTP provider error occupied";

            /// <summary>
            /// "Imported file returns empty data"
            /// </summary>
            public static string ImportFileEmptyData => "Imported file returns empty data";

            /// <summary>
            /// "Some errors occurred while checking the entity"
            /// </summary>
            public static string EntityValidationException => "Some errors occurred while checking the entity";

            /// <summary>
            /// "Some errors occurred while importing data"
            /// </summary>
            public static string ImportGeneralException => "Some errors occurred while importing data";

            /// <summary>
            /// "Invalid operation exception was thrown"
            /// </summary>
            public static string InvalidOperationException => "Invalid operation exception was thrown";

            /// <summary>
            /// "Template {0} for builder not found"
            /// </summary>
            public static string TemplateNotFoundForBuilder => "Template {0} for builder not found";

            /// <summary>
            /// "Argument out of range"
            /// </summary>
            public static string ArgumentOutOfRangeException => "Argument out of range";

            /// <summary>
            /// "The NotificationEventType should not be None";
            /// </summary>
            public static string NotificationEventTypeInvalid => "The NotificationEventType should not be None";

            /// <summary>
            /// "Error occurred while trying to send push nNotification"
            /// </summary>
            public static string SendPushNotificationFailed => "Error occurred while trying to send push nNotification";

            /// <summary>
            /// "Notification {0} not found"
            /// </summary>
            public static string NotificationNotFound => "Notification {0} not found";

            /// <summary>
            /// "Notification does not contains any protocols";
            /// </summary>
            public static string NotificationDoesNotContainsProtocols => "Notification does not contains any protocols";

            /// <summary>
            /// "Object was not initialized before use"
            /// </summary>
            public static string NotInitializedException => "Object was not initialized before use";

            /// <summary>
            /// "Hotel identifier not found for exchange operation"
            /// </summary>
            public static string HotelIdentifierRequiredForExchange => "Hotel identifier not found for exchange operation";

            /// <summary>
            /// "Communication message sender not found"
            /// </summary>
            public static string CommunicationSenderNotFound => "Communication message sender not found";

            /// <summary>
            /// "Can't reset password"
            /// </summary>
            public static string ResetPasswordException => "Can't reset password";

            /// <summary>
            /// "Competitor hotel not found"
            /// </summary>
            public static string CompetitorHotelNotFoundException => "Competitor hotel not found";

            /// <summary>
            /// "Competitor room type not found";
            /// </summary>
            public static string CompetitorRoomTypeNotFoundException => "Competitor room type not found";

            /// <summary>
            /// "Hotel identifier not provided. Work should be deleted"
            /// </summary>
            public static string WorkDeleted => "Hotel identifier not provided. Work should be deleted";
        }
    }
}
