using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace $safeprojectname$.AppStart
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
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
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
                }
            };
        }

        /// <summary>
        /// scopes define the resources in your system
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        }

        /// <summary>
        /// IdentityServer4 API resources
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "API Default")
            };
        }

        public static IEnumerable<ApiScope> GetAPiScopes()
        {
            yield return new ApiScope("api1");
        }
    }
}
