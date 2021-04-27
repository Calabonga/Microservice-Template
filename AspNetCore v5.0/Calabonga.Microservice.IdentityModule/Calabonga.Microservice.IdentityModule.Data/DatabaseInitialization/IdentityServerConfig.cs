using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Calabonga.Microservice.IdentityModule.Data.DatabaseInitialization
{
    /// <summary>
    /// IdentityServer configuration
    /// </summary>
    public class IdentityServerConfig
    {
        /// <summary>
        /// clients want to access resources (aka scopes)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients() =>
            // client credentials client
            new List<Client>
        {
            // resource owner password grant client
            // you can create your own client
            new Client
            {
                ClientId = "microservice1",
                AllowAccessTokensViaBrowser = true,
                IdentityTokenLifetime = 21600,
                AuthorizationCodeLifetime = 21600,
                AccessTokenLifetime = 21600,
                AllowOfflineAccess =  true,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 1296000, //in seconds = 15 days
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Address,
                    "api1"
                }
            },
            new Client
            {
                ClientId = "blazor_web_assembly",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Address,
                    "api1"
                },
                RedirectUris = { "https://localhost:5001/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:5001" },
            }
        };

        /// <summary>
        /// scopes define the resources in your system
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        /// <summary>
        /// IdentityServer4 API resources
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new ApiResource("api1", "API Default")
        };

        public static IEnumerable<ApiScope> GetAPiScopes()
        {
            yield return new ApiScope("api1");
        }
    }
}
