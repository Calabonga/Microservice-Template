using Calabonga.AuthService.Infrastructure;
using OpenIddict.Abstractions;

namespace Calabonga.AuthService.Web.HostedServices
{
    public class OpenIddictWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public OpenIddictWorker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("service-to-service", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    DisplayName = "Service-To-Service demonstration",
                    Permissions =
                    {
                        // Endpoint permissions
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        // Grant type permissions
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        
                        // Scope permissions
                        OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                    }
                }, cancellationToken);
            }
            
            if (await manager.FindByClientIdAsync("authorization-flow", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "thunder_client",
                    ClientSecret = "thunder_client_secret",
                    DisplayName = "Thunder Client with Authorization Code Flow demonstration",
                    RedirectUris = { new Uri("https://www.thunderclient.com/oauth/callback") },
                    Permissions =
                    {
                        // Endpoint permissions
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        // Grant type permissions
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,

                        // Scope permissions
                        OpenIddictConstants.Permissions.Prefixes.Scope + "api",

                        // Response types
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.IdToken
                    }
                }, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
