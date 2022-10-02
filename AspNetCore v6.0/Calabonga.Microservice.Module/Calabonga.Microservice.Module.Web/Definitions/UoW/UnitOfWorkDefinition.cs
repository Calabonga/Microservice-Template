using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.Module.Infrastructure;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.Module.Web.Definitions.UoW;

/// <summary>
/// Unit of Work registration as MicroserviceDefinition
/// </summary>
public class UnitOfWorkDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        => services.AddUnitOfWork<ApplicationDbContext>();
}