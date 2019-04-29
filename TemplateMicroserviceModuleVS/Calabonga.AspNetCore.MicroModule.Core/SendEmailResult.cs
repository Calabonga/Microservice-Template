using System;

namespace $safeprojectname$
{
    /// <summary>
    /// MailKit send result
    /// </summary>
    public class SendEmailResult
    {
        /// <summary>
        /// Indicate mail sent
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// Indicate sending in process
        /// </summary>
        public bool IsInProcess { get; set; }

        /// <summary>
        /// Exception while sending
        /// </summary>
        public Exception Exception { get; set; }
    }
}