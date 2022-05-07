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

            //await manager.CreateAsync(new OpenIddictApplicationDescriptor
            //{
            //    ClientId = "console_app",
            //    RedirectUris = { new Uri("http://localhost:8739/") },
            //    Permissions =
            //{
            //    OpenIddictConstants.Permissions.Endpoints.Authorization,
            //    OpenIddictConstants.Permissions.Endpoints.Token,
            //    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
            //    OpenIddictConstants.Permissions.ResponseTypes.Code,
            //    OpenIddictConstants.Permissions.Scopes.Email,
            //    OpenIddictConstants.Permissions.Scopes.Profile,
            //    OpenIddictConstants.Permissions.Scopes.Roles,
            //    OpenIddictConstants.Permissions.Prefixes.Scope + "api1",
            //    OpenIddictConstants.Permissions.Prefixes.Scope + "api2"
            //}
            //}, cancellationToken);

            if (await manager.FindByClientIdAsync("clientcredentials", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    DisplayName = "Client Credentials DEMO",
                    Permissions =
                    {
                        //Endpoint permissions
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        //Grant type permissions
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        
                        // Scope permissions
                        OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                    }
                }, cancellationToken);
            }

            //if (await manager.FindByClientIdAsync("resource_server_1", cancellationToken) is null)
            //{
            //    await manager.CreateAsync(new OpenIddictApplicationDescriptor
            //    {
            //        ClientId = "resource_server_1",
            //        ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
            //        Permissions =
            //    {
            //        OpenIddictConstants.Permissions.Endpoints.Introspection
            //    }
            //    }, cancellationToken);
            //}
            //if (await manager.FindByClientIdAsync("postman", cancellationToken) is null)
            //{
            //    await manager.CreateAsync(new OpenIddictApplicationDescriptor
            //    {
            //        ClientId = "postman",
            //        ClientSecret = "postman-secret",
            //        DisplayName = "Postman",
            //        Permissions =
            //    {
            //        OpenIddictConstants.Permissions.Endpoints.Token,
            //        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
            //        OpenIddictConstants.Permissions.Prefixes.Scope + "api"
            //    }
            //    }, cancellationToken);
            //}


        }

        public Task StopAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
