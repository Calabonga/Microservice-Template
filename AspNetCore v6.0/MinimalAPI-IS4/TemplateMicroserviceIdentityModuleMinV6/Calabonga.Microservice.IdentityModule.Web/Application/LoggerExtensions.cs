using System.Diagnostics;

namespace $safeprojectname$.Application
{
    /// <summary>
    /// The number identifiers for events in the microservices
    /// </summary>
    internal static class EventNumbers
    {
        internal static readonly EventId UserRegistrationId = new EventId(9001, nameof(UserRegistrationId));
        internal static readonly EventId PostItemId = new EventId(9002, nameof(PostItemId));
    }

    /// <summary>
    /// Event logging as ILogger extension.
    /// Please see the video as the manual https://youtu.be/09EVKgHgwnM
    /// </summary>
    internal static class LoggerExtensions
    {
        #region UserRegistration

        /// <summary>
        /// EventItem register action event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="userName"></param>
        /// <param name="exception"></param>
        internal static void MicroserviceUserRegistration(this ILogger source, string userName, Exception? exception = null)
        {
            switch (exception)
            {
                case null:
                    UserRegistrationExecute(source, userName, exception);
                    break;
                default:
                    UserRegistrationFailedExecute(source, userName, exception);
                    break;
            }
        }

        private static readonly Action<ILogger, string, Exception?> UserRegistrationExecute =
            LoggerMessage.Define<string>(LogLevel.Information, EventNumbers.UserRegistrationId,
                "User {userName} successfully registred");

        private static readonly Action<ILogger, string, Exception?> UserRegistrationFailedExecute =
            LoggerMessage.Define<string>(LogLevel.Error, EventNumbers.UserRegistrationId,
                "User {userName} registred failed");

        #endregion

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

        #region LogTimeElapsed

        /// <summary>
        /// Elapsed milliseconds show in debug mode 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static IDisposable ShowMillisecondsElapsed<T>(
            this ILogger<T> logger,
            string? message, params object?[] args)
            => new LogElapsedOperation<T>(logger, LogLevel.Debug, message, args);

        #endregion
    }

    /// <summary>
    /// Logging for Elapsed milliseconds when operation executing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class LogElapsedOperation<T> : IDisposable
    {
        private readonly ILogger<T> _logger;
        private readonly LogLevel _logLevel;
        private readonly string? _message;
        private readonly object?[] _arguments;
        private readonly Stopwatch _stopwatch;

        public LogElapsedOperation(
            ILogger<T> logger,
            LogLevel logLevel,
            string? message,
            object?[] arguments)
        {
            _logger = logger;
            _logLevel = logLevel;
            _message = message;
            _arguments = arguments;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _logger.Log(_logLevel, $"{_message} completed in {_stopwatch.ElapsedMilliseconds}ms", _arguments);
        }
    }
}