using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Web.Definitions.FluentValidating;
using Mediator;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator;

/// <summary>
/// Register Mediator as application definition
/// </summary>
public class MediatorDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current application
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

    }
}
