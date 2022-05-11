namespace Calabonga.Microservice.Module.Web.Endpoints.Base
{
    /// <summary>
    /// Microservice endpoint interface abstraction
    /// </summary>
    public interface IEndpointDefinition
    {
        /// <summary>
        /// Microservice configuration setup
        /// </summary>
        /// <param name="app"></param>
        void ConfigureApplication(WebApplication app);

        /// <summary>
        /// Microservice dependency injection configuration setup
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
