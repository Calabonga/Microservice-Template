using Calabonga.Microservice.IdentityModule.Web.Mediator.Base;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.CreateSteps
{
    /// <summary>
    /// Step for Create pipeline 4
    /// </summary>
    public class OnCreateOperationResultBuildStep: HandlerStep
    {
        /// <inheritdoc />
        public OnCreateOperationResultBuildStep(IHandlerContext context)
        {
            Context = context;
        }

        /// <inheritdoc />
        protected override int OrderIndex => 4;
    }
}