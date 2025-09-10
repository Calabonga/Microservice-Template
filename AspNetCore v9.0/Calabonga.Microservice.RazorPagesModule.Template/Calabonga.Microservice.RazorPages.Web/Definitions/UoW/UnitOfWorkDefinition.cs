using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.RazorPages.Infrastructure;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.RazorPages.Web.Definitions.UoW;

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
