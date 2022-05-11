using Calabonga.Microservice.IdentityModule.Web.Application.Services;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Base;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Identity;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.DependencyContainer;

/// <summary>
/// Dependency container definition
/// </summary>
public class ContainerDefinition: AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ApplicationUserClaimsPrincipalFactory>();
    }
}