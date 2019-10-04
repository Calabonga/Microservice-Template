using System;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservices.Core
{
    /// <summary>
    /// Run Asynchronous methods as Synchronous.
    /// </summary>
    public static class AsyncHelper
    {
        private static readonly TaskFactory AppTaskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        /// <summary>
        /// Run asynchronous method as synchronous
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return AppTaskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Run asynchronous method as synchronous
        /// </summary>
        /// <param name="func"></param>
        public static void RunSync(Func<Task> func)
        {
            AppTaskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}
