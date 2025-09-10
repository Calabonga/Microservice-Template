using Calabonga.AspNetCore.AppDefinitions;

namespace Calabonga.Microservice.RazorPages.Web.Definitions.Mediator;

/// <summary>
/// Register Mediator as MicroserviceDefinition
/// </summary>
public class MediatorDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddMediator(cfg => cfg.ServiceLifetime = ServiceLifetime.Scoped);
    }
}
