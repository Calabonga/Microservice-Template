using System;
using Microsoft.Extensions.Logging;

namespace $safeprojectname$.Infrastructure.EventLogs
{
    /// <summary>
    /// Event logging as ILogger extension.
    /// Please see the video as the manual https://youtu.be/09EVKgHgwnM
    /// </summary>
    internal static class LoggerExtensions
    {
        #region PostItem

        internal static void MicroservicePostItem(this ILogger source, string entityName, Exception? exception = null)
        {
            switch (exception)
            {
                case null:
                    PostItemExecute(source, entityName, null);
                    break;

                default:
                    PostItemFailedExecute(source, entityName, exception);
                    break;
            }
        }

        private static readonly Action<ILogger, string, Exception?> PostItemExecute =
            LoggerMessage.Define<string>(LogLevel.Information, EventNumbers.PostItemId,
                "The {entityName} successfully saved");

        private static readonly Action<ILogger, string, Exception?> PostItemFailedExecute =
            LoggerMessage.Define<string>(LogLevel.Error, EventNumbers.PostItemId,
                "The {entityName} saving failed");

        #endregion
    }
}
