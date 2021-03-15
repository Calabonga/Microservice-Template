using Calabonga.Microservice.IdentityModule.Web.Mediator.Base;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.UpdateSteps
{
    /// <summary>
    /// Update step 1
    /// </summary>
    public class OnUpdateBeforeMappingStep : HandlerStep
    {
        /// <inheritdoc />
        public OnUpdateBeforeMappingStep(IHandlerContext context)
        {
            Context = context;
        }

        /// <inheritdoc />
        protected override int OrderIndex => 1;
    }
}