// unset

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Base
{
    /// <summary>
    /// Result of the pipeline
    /// </summary>
    public class HandlerResult: HandlerStep
    {
        protected HandlerResult()
        {
            // Result pipeline should stop process
            Stop();
        }

        /// <inheritdoc />
        protected override int OrderIndex => 6;
    }
}