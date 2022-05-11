using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Base;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.UoW
{
    /// <summary>
    /// Unit of Work registration as MicroserviceDefinition
    /// </summary>
    public class UnitOfWorkMicroserviceDefinition : MicroserviceDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
            => services.AddUnitOfWork<ApplicationDbContext>();
    }
}
