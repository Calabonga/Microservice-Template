using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints;

/// <summary>
/// Authorize Endpoint for OpenIddict
/// You can test your authorization server with https://oidcdebugger.com/
/// You can mock your authorization flow with https://oauth.mocklab.io/
/// </summary>
public sealed class AuthorizeEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("~/connect/authorize", AuthorizeAsync).ExcludeFromDescription().AllowAnonymous();
        app.MapPost("~/connect/authorize", AuthorizeAsync).ExcludeFromDescription().AllowAnonymous();
    }

    private async Task<IResult> AuthorizeAsync(
        HttpContext httpContext,
        IOpenIddictScopeManager scopeManager,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOpenIddictApplicationManager applicationManager,
        IOpenIddictAuthorizationManager authorizationManager)
    {
        var request = httpContext.Request;
        var iddictRequest = httpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        // Retrieve the user principal stored in the authentication cookie.
        var result = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            return Results.Challenge(new AuthenticationProperties
            {
                RedirectUri = request.PathBase + request.Path + QueryString.Create(request.HasFormContentType
                ? request.Form.ToList()
                : request.Query.ToList())
            },
            new List<string> { CookieAuthenticationDefaults.AuthenticationScheme });
        }

        // ATTENTION:  If you use are "IN-Memory" mode, then system cannot track user that recreated every time on start. You should clear cookies (site data) in browser.
        var user = await userManager.GetUserAsync(result.Principal) ?? throw new InvalidOperationException("The user details cannot be retrieved.");

        var application = await applicationManager.FindByClientIdAsync(iddictRequest.ClientId!) ?? throw new InvalidOperationException("Details concerning the calling client application cannot be found.");
        var applicationId = await applicationManager.GetIdAsync(application);
        var userId = await userManager.GetUserIdAsync(user);

        var authorizations = await authorizationManager.FindAsync(
        subject: userId,
        client: applicationId!,
        status: OpenIddictConstants.Statuses.Valid,
        type: OpenIddictConstants.AuthorizationTypes.Permanent,
        scopes: iddictRequest.GetScopes()).ToListAsync();

        switch (await applicationManager.GetConsentTypeAsync(application))
        {
            // If the consent is external (e.g when authorizations are granted by a sysadmin),
            // immediately return an error if no authorization can be found in the database.
            case OpenIddictConstants.ConsentTypes.External when !authorizations.Any():
                return Results.Forbid(
                        authenticationSchemes: new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                        properties: new AuthenticationProperties(new Dictionary<string, string>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.ConsentRequired,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                                "The logged in user is not allowed to access this client application."
                        }!));

            // If the consent is implicit or if an authorization was found,
            // return an authorization response without displaying the consent form.
            case OpenIddictConstants.ConsentTypes.Implicit:
            case OpenIddictConstants.ConsentTypes.External when authorizations.Any():
            case OpenIddictConstants.ConsentTypes.Explicit when authorizations.Any() && !iddictRequest.HasPrompt(OpenIddictConstants.Prompts.Consent):

                var principal = await signInManager.CreateUserPrincipalAsync(user);

                // Note: in this sample, the granted scopes match the requested scope
                // but you may want to allow the user to uncheck specific scopes.
                // For that, simply restrict the list of scopes before calling SetScopes.

                principal.SetScopes(iddictRequest.GetScopes());
                principal.SetResources(await scopeManager.ListResourcesAsync(principal.GetScopes()).ToListAsync());

                // Automatically create a permanent authorization to avoid requiring explicit consent
                // for future authorization or token requests containing the same scopes.
                var authorization = authorizations.LastOrDefault() ?? await authorizationManager.CreateAsync(
                    principal: principal,
                    subject: await userManager.GetUserIdAsync(user),
                    client: applicationId!,
                    type: OpenIddictConstants.AuthorizationTypes.Permanent,
                    scopes: principal.GetScopes());

                principal.SetAuthorizationId(await authorizationManager.GetIdAsync(authorization));

                principal.SetDestinations(static claim => claim.Type switch
                {
                    // If the "profile" scope was granted, allow the "name" claim to be
                    // added to the access and identity tokens derived from the principal.
                    OpenIddictConstants.Claims.Name when claim.Subject!.HasScope(OpenIddictConstants.Scopes.Profile) => new[]
                    {
                        OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken
                    },

                    // Never add the "secret_value" claim to access or identity tokens.
                    // In this case, it will only be added to authorization codes,
                    // refresh tokens and user/device codes, that are always encrypted.
                    "secret_value" => Array.Empty<string>(),

                    // Otherwise, add the claim to the access tokens only.
                    _ => new[] { OpenIddictConstants.Destinations.AccessToken }
                });



                return Results.SignIn(principal, null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            // At this point, no authorization was found in the database and an error must be returned
            // if the client application specified prompt=none in the authorization request.
            case OpenIddictConstants.ConsentTypes.Explicit when iddictRequest.HasPrompt(OpenIddictConstants.Prompts.None):
            case OpenIddictConstants.ConsentTypes.Systematic when iddictRequest.HasPrompt(OpenIddictConstants.Prompts.None):
                return Results.Forbid(
                        authenticationSchemes: new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                        properties: new AuthenticationProperties(new Dictionary<string, string>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.ConsentRequired,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                                "Interactive user consent is required."
                        }!));

            // In every other case, render the consent form.
            default:
                return Results.Challenge(
                    authenticationSchemes: new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                    properties: new AuthenticationProperties { RedirectUri = "/" });
        }
    }
}