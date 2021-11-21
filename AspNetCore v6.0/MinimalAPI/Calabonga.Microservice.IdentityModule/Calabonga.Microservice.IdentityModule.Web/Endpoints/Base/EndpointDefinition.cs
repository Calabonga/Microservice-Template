namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.Base
{
    /// <summary>
    /// Microservice endpoint interface implementation
    /// </summary>
    public abstract class EndpointDefinition : IEndpointDefinition
    {
        /// <summary>
        /// Microservice configuration setup
        /// </summary>
        /// <param name="app"></param>
        public virtual void ConfigureApplication(WebApplication app)
        {

        }

        /// <summary>
        /// Microservice dependency injection configuration setup
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}