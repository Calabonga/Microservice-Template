using Calabonga.AuthService.Web.Application.Services;
using Calabonga.AuthService.Web.Definitions.Base;
using Calabonga.AuthService.Web.Definitions.Identity;

namespace Calabonga.AuthService.Web.Definitions.DependencyContainer;

/// <summary>
/// Dependency container definition
/// </summary>
public class ContainerDefinition: AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ApplicationClaimsPrincipalFactory>();
    }
}