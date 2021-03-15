using Calabonga.Microservice.IdentityModule.Web.Mediator.Base;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.UpdateSteps
{
    /// <summary>
    /// On UpdateBeforeSaveChanges Step
    /// </summary>
    public class OnUpdateBeforeSaveChangesStep: HandlerStep
    {
        /// <inheritdoc />
        public OnUpdateBeforeSaveChangesStep(IHandlerContext context)
        {
            Context = context;
        }

        /// <inheritdoc />
        protected override int OrderIndex => 3;
    }
}