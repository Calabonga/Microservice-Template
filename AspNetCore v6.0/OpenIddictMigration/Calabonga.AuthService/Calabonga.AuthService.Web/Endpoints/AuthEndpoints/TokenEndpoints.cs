using Calabonga.AuthService.Web.Definitions.Base;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace Calabonga.AuthService.Web.Endpoints.AuthEndpoints
{
    public class TokenEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment)
        {
            app.MapPost("~/connect/token", Exchange);
        }

        //[HttpPost("~/connect/token")]
        //[Consumes("application/x-www-form-urlencoded")]
        //[Produces("application/json")]
        public IResult Exchange(HttpContext httpContext, IOpenIddictScopeManager manager)
        {
            var request = httpContext.GetOpenIddictServerRequest() ??
                          throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            ClaimsPrincipal claimsPrincipal;

            if (request.IsClientCredentialsGrantType())
            {
                // Note: the client credentials are automatically validated by OpenIddict:
                // if client_id or client_secret are invalid, this action won't be invoked.

                var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                // Subject (sub) is a required field, we use the client id as the subject identifier here.
                identity.AddClaim(OpenIddictConstants.Claims.Subject, request.ClientId ?? throw new InvalidOperationException());

                // Add some claim, don't forget to add destination otherwise it won't be added to the access token.
                identity.AddClaim("some-claim", "some-value", OpenIddictConstants.Destinations.AccessToken);

                

                claimsPrincipal = new ClaimsPrincipal(identity);

                claimsPrincipal.SetScopes(request.GetScopes());
            }

            else
            {
                throw new InvalidOperationException("The specified grant type is not supported.");
            }

            // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
            return Results.SignIn(claimsPrincipal,new AuthenticationProperties(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }


        //[HttpPost]
        //[Produces("application/json")]
        //private IResult Exchange(HttpContext httpContext, IOpenIddictScopeManager manager)
        //{
        //    var request = httpContext.GetOpenIddictServerRequest() ??
        //                  throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        //    ClaimsPrincipal claimsPrincipal;

        //    if (request.IsClientCredentialsGrantType())
        //    {
        //        // Note: the client credentials are automatically validated by OpenIddict:
        //        // if client_id or client_secret are invalid, this action won't be invoked.

        //        var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        //        // Subject (sub) is a required field, we use the client id as the subject identifier here.
        //        identity.AddClaim(OpenIddictConstants.Claims.Subject, request.ClientId ?? throw new InvalidOperationException());

        //        // Add some claim, don't forget to add destination otherwise it won't be added to the access token.
        //        identity.AddClaim("some-claim", "some-value", OpenIddictConstants.Destinations.AccessToken);

        //        claimsPrincipal = new ClaimsPrincipal(identity);

        //        claimsPrincipal.SetScopes(request.GetScopes());
        //    }

        //    else
        //    {
        //        throw new InvalidOperationException("The specified grant type is not supported.");
        //    }

        //    // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
        //    return  Results.SignIn(claimsPrincipal, null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        //}
    }
}
