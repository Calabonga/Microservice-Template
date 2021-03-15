using System;
using System.Diagnostics;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Base
{
    /// <summary>
    /// All step for modification are returns this type of the operation
    /// </summary>
    [DebuggerDisplay("Stopped {IsStopped} Index {OrderIndex}")]
    public abstract class HandlerStep : IHandlerStep
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected HandlerStep() { }

        /// <summary>
        /// Writable controller context
        /// </summary>
        public IHandlerContext Context { get; set; }

        /// <summary>
        /// Pipeline operation executing order index
        /// </summary>
        protected abstract int OrderIndex { get; }

        /// <summary>
        /// Returns true when developer need to stop other operation and return result
        /// </summary>
        public bool IsStopped { get; private set; }

        /// <summary>
        /// Stop pipeline
        /// </summary>
        public void Stop()
        {
            IsStopped = true;
        }

        /// <inheritdoc />
        public void StopWithError(Exception exception, object dataObject = null)
        {
            Context.AddError(exception, dataObject);
            IsStopped = true;
        }

        /// <inheritdoc />
        public void StopWithError(string errorMessage, object dataObject = null)
        {
            Context.AddError(errorMessage, dataObject);
            IsStopped = true;
        }

        /// <inheritdoc />
        public void StopWithWarning(string warningMessage, object dataObject = null)
        {
            Context.AddWarning(warningMessage, dataObject);
            IsStopped = true;
        }

        /// <inheritdoc />
        public void StopWithSuccess(string successMessage, object dataObject = null)
        {
            Context.AddSuccess(successMessage, dataObject);
            IsStopped = true;
        }

        /// <inheritdoc />
        public void StopWithInfo(string infoMessage, object dataObject = null)
        {
            Context.AddInfo(infoMessage, dataObject);
            IsStopped = true;
        }
    }
}
