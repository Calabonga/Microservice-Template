using Calabonga.Microservice.IdentityModule.Infrastructure;
using OpenIddict.Abstractions;

namespace Calabonga.Microservice.IdentityModule.Web.HostedServices;

public class OpenIddictWorker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public OpenIddictWorker(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        // credentials password
        const string client_id1 = "client-id-sts";
        if (await manager.FindByClientIdAsync(client_id1, cancellationToken) is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = client_id1,
                ClientSecret = "client-secret-sts",
                DisplayName = "Service-To-Service demonstration",
                Permissions =
                {
                    // Endpoint permissions
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    // Grant type permissions
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.GrantTypes.Password,

                    // Scope permissions
                    OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                }
            }, cancellationToken);
        }

        const string client_id2 = "client-id-code";
        if (await manager.FindByClientIdAsync(client_id2, cancellationToken) is null)
        {
            var url = _serviceProvider.GetRequiredService<IConfiguration>().GetValue<string>("AuthServer:Url");

            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = client_id2,
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                ClientSecret = "client-secret-code",
                DisplayName = "API testing clients with Authorization Code Flow demonstration",
                RedirectUris = {
                    new Uri("https://www.thunderclient.com/oauth/callback"),            // https://www.thunderclient.com/
                    new Uri($"{url}/swagger/oauth2-redirect.html"),                     // https://swagger.io/
                    new Uri("https://localhost:20001/swagger/oauth2-redirect.html")     // https://swagger.io/ for Module
                },

                Permissions =
                {
                    // Endpoint permissions
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    // Grant type permissions
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                    // Scope permissions
                    OpenIddictConstants.Permissions.Prefixes.Scope + "api",
                    OpenIddictConstants.Permissions.Prefixes.Scope + "custom",

                    // Response types
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.ResponseTypes.IdToken
                }
            }, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}