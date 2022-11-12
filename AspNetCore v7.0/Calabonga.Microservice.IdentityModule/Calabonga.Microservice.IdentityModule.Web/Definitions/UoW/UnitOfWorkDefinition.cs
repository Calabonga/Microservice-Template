using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.UoW;

/// <summary>
/// Unit of Work registration as application definition
/// </summary>
public class UnitOfWorkDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        => services.AddUnitOfWork<ApplicationDbContext>();
}