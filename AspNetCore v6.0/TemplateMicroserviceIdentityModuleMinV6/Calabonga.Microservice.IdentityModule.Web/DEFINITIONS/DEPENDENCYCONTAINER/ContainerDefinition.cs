using Calabonga.AspNetCore.AppDefinitions;
using $safeprojectname$.Application.Services;
using $safeprojectname$.Definitions.Identity;

namespace $safeprojectname$.Definitions.DependencyContainer;

/// <summary>
/// Dependency container definition
/// </summary>
public class ContainerDefinition: AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ApplicationUserClaimsPrincipalFactory>();
    }
}