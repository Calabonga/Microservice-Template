using $safeprojectname$.Application.Services;
using $safeprojectname$.Definitions.Base;
using $safeprojectname$.Definitions.Identity;

namespace $safeprojectname$.Definitions.DependencyContainer;

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