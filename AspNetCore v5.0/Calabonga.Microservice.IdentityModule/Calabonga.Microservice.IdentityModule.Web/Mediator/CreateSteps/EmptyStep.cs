using Calabonga.Microservice.IdentityModule.Web.Mediator.Base;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.CreateSteps
{
    /// <summary>
    /// Marker for empty step
    /// </summary>
    public class EmptyStep : HandlerStep
    {
        /// <summary>
        /// Pipeline operation executing order index
        /// </summary>
        protected override int OrderIndex => -1;
    }
}
