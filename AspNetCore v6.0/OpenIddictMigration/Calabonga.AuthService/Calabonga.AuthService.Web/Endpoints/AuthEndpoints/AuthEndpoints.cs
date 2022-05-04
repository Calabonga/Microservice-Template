using Calabonga.AuthService.Web.Definitions.Base;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Globalization;

namespace Calabonga.AuthService.Web.Endpoints.AuthEndpoints
{
    class MyClass : Controller
    {
        public override SignInResult SignIn(ClaimsPrincipal principal) => base.SignIn(principal);
    }
    public class AuthEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment)
        {

            //app.MapGet("/authorize", async () =>
            //{
            //    // Retrieve the OpenIddict server request from the HTTP context.
            //    var request = context.GetOpenIddictServerRequest();

            //    var identifier = (int?)request["hardcoded_identity_id"];
            //    if (identifier is not (1 or 2))
            //    {
            //        return Results.Challenge(
            //            authenticationSchemes: new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
            //            properties: new AuthenticationProperties(new Dictionary<string, string>
            //            {
            //                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidRequest,
            //                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The specified hardcoded identity is invalid."
            //            }));
            //    }

            //    // Create a new identity and populate it based on the specified hardcoded identity identifier.
            //    var identity = new ClaimsIdentity(TokenValidationParameters.DefaultAuthenticationType);
            //    identity.AddClaim(new Claim(OpenIddictConstants.Claims.Subject, identifier.Value.ToString(CultureInfo.InvariantCulture)));
            //    identity.AddClaim(new Claim(OpenIddictConstants.Claims.Name, identifier switch
            //    {
            //        1 => "Alice",
            //        2 => "Bob",
            //        _ => throw new InvalidOperationException()
            //    }).SetDestinations(OpenIddictConstants.Destinations.AccessToken));

            //    // Note: in this sample, the client is granted all the requested scopes for the first identity (Alice)
            //    // but for the second one (Bob), only the "api1" scope can be granted, which will cause requests sent
            //    // to Zirku.Api2 on behalf of Bob to be automatically rejected by the OpenIddict validation handler,
            //    // as the access token representing Bob won't contain the "resource_server_2" audience required by Api2.
            //    var principal = new ClaimsPrincipal(identity);

            //    principal.SetScopes(identifier switch
            //    {
            //        1 => request.GetScopes(),
            //        2 => new[] { "api1" }.Intersect(request.GetScopes()),
            //        _ => throw new InvalidOperationException()
            //    });

            //    var d = await manager.ListResourcesAsync(principal.GetScopes());
            //    principal.SetResources().ToListAsync(d);

            //    return Results.SignIn(principal, properties: null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            //});





















            app.MapGet("~/connect/token", Exchange);
        }

        [HttpPost]
        private IResult Exchange(HttpContext httpContext, IOpenIddictScopeManager manager)
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
            return  Results.SignIn(claimsPrincipal, null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
