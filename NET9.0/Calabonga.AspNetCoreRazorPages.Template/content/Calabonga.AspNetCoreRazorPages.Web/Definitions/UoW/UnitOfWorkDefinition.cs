using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.AspNetCoreRazorPages.Infrastructure;
using Calabonga.UnitOfWork;

namespace Calabonga.AspNetCoreRazorPages.Web.Definitions.UoW;

/// <summary>
/// Unit of Work registration as MicroserviceDefinition
/// </summary>
public class UnitOfWorkDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddUnitOfWork<ApplicationDbContext>();
    }
}
