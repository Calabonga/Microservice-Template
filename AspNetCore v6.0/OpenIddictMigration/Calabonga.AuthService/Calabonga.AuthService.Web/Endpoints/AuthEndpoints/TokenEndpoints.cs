using Calabonga.AuthService.Domain.Base;
using Calabonga.AuthService.Web.Definitions.Base;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace Calabonga.AuthService.Web.Endpoints.AuthEndpoints;

/// <summary>
/// // Calabonga: update summary (2022-05-07 01:01 TokenEndpoints)
/// </summary>
public class TokenEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment) =>
        app.MapPost("~/connect/token", TokenAsync);

    private async Task<IResult> TokenAsync(HttpContext httpContext, IOpenIddictScopeManager manager)
    {
        var request = httpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        ClaimsPrincipal? claimsPrincipal;

        if (request.IsClientCredentialsGrantType())
        {
            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            // Subject or sub is a required field, we use the client id as the subject identifier here.
            identity.AddClaim(OpenIddictConstants.Claims.Subject, AppData.ServiceName);

            // Add some claim, don't forget to add destination otherwise it won't be added to the access token.
            identity.AddClaim("nimble", "framework", OpenIddictConstants.Destinations.AccessToken);

            claimsPrincipal = new ClaimsPrincipal(identity);

            claimsPrincipal.SetScopes(request.GetScopes());
        }
        else if (request.IsAuthorizationCodeGrantType())
        {
            claimsPrincipal = (await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
        }
        else
        {
            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
        return Results.SignIn(claimsPrincipal!, new AuthenticationProperties(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}