using Calabonga.AuthService.Web.Definitions.Base;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Calabonga.AuthService.Web.Endpoints.AuthEndpoints;

/// <summary>
/// Authorize Endpoint for OpenIddict
/// You can test your authorization server https://oidcdebugger.com/
/// </summary>
public class AuthorizeEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment)
    {
        app.MapGet("~/connect/authorize", AuthorizeAsync).ExcludeFromDescription();
        app.MapPost("~/connect/authorize", AuthorizeAsync).ExcludeFromDescription();
    }

    private async Task<IResult> AuthorizeAsync(HttpContext httpContext, IOpenIddictScopeManager manager)
    {
        var request = httpContext.Request;
        var iddictRequest = httpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        // Retrieve the user principal stored in the authentication cookie.
        var result = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // If the user principal can't be extracted, redirect the user to the login page.
        if (!result.Succeeded)
        {
            return Results.Challenge(new AuthenticationProperties
            {
                RedirectUri = request.PathBase + request.Path + QueryString.Create(request.HasFormContentType ? request.Form.ToList() : request.Query.ToList())
            },
                new List<string> { CookieAuthenticationDefaults.AuthenticationScheme });
        }


        foreach (var claim in result.Principal.Claims)
        {
            claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken);
        }

        // Set requested scopes (this is not done automatically)
        result.Principal.SetScopes(iddictRequest.GetScopes());

        // Signing in with the OpenIddict authentication scheme trigger OpenIddict to issue a code (which can be exchanged for an access token)
        return Results.SignIn(result.Principal, result.Properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}