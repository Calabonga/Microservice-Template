using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Web.Application.Services;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Identity;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.DependencyContainer;

/// <summary>
/// Dependency container definition
/// </summary>
public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<ApplicationUserClaimsPrincipalFactory>();
    }
}