using $ext_projectname$.Domain.Base;
using $safeprojectname$.Definitions.Base;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace $safeprojectname$.Endpoints.AuthEndpoints;

/// <summary>
/// Token Endpoint for OpenIddict
/// </summary>
public class TokenEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment) =>
        app.MapPost("~/connect/token", TokenAsync).ExcludeFromDescription();

    private async Task<IResult> TokenAsync(HttpContext httpContext, IOpenIddictScopeManager manager)
    {
        var request = httpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        ClaimsPrincipal? claimsPrincipal;
        AuthenticationProperties? properties = null;

        if (request.IsClientCredentialsGrantType() || request.IsPasswordGrantType())
        {
            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            // Subject or sub is a required field, we use the client id as the subject identifier here.
            identity.AddClaim(OpenIddictConstants.Claims.Subject, AppData.ServiceName);

            // Don't forget to add destination otherwise it won't be added to the access token.
            identity.AddClaim("nimble", "framework", OpenIddictConstants.Destinations.AccessToken);

            claimsPrincipal = new ClaimsPrincipal(identity);

            claimsPrincipal.SetScopes(request.GetScopes());
        }
        else if (request.IsAuthorizationCodeGrantType())
        {
            var authenticateResult = await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            properties = authenticateResult.Properties;
            claimsPrincipal = authenticateResult.Principal;
        }
        else
        {
            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
        return Results.SignIn(claimsPrincipal!, properties ?? new AuthenticationProperties(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}