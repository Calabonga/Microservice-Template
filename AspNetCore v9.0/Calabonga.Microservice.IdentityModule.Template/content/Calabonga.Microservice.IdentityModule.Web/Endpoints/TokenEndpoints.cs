using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Application.Services;
using Calabonga.Utils.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints;

/// <summary>
/// Token Endpoint for OpenIddict
/// </summary>
public sealed class TokenEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app) =>
        app.MapPost("~/connect/token", async (
                    HttpContext httpContext,
                    UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager,
                    IAccountService accountService) =>
            {
                var request = httpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

                if (request.IsClientCredentialsGrantType())
                {
                    var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                    // Subject or sub is a required field, we use the client id as the subject identifier here.
                    identity.AddClaim(OpenIddictConstants.Claims.Subject, request.ClientId!);
                    identity.AddClaim(OpenIddictConstants.Claims.ClientId, request.ClientId!);

                    // Don't forget to add destination otherwise it won't be added to the access token.
                    if (request.Scope.IsNullOrEmpty())
                    {
                        identity.AddClaim(OpenIddictConstants.Claims.Scope, request.Scope!, OpenIddictConstants.Destinations.AccessToken);
                    }

                    identity.AddClaim("nimble", "framework", OpenIddictConstants.Destinations.AccessToken);

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    claimsPrincipal.SetScopes(request.GetScopes());
                    return Results.SignIn(claimsPrincipal, new AuthenticationProperties(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                if (request.IsPasswordGrantType())
                {
                    if (request.Username != null)
                    {
                        var user = await userManager.FindByNameAsync(request.Username);
                        if (user == null)
                        {
                            return Results.Problem("Invalid operation");
                        }

                        // Ensure the user is allowed to sign in
                        if (!await signInManager.CanSignInAsync(user))
                        {
                            return Results.Problem("Invalid operation");
                        }


                        // Ensure the user is not already locked out
                        if (userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user))
                        {
                            return Results.Problem("Invalid operation");
                        }

                        // Ensure the password is valid
                        if (request.Password != null && !await userManager.CheckPasswordAsync(user, request.Password))
                        {
                            if (userManager.SupportsUserLockout)
                            {
                                await userManager.AccessFailedAsync(user);
                            }

                            return Results.Problem("Invalid operation");
                        }

                        // Reset the lockout count
                        if (userManager.SupportsUserLockout)
                        {
                            await userManager.ResetAccessFailedCountAsync(user);
                        }

                        var principal = await accountService.GetPrincipalForUserAsync(user);

                        return Results.SignIn(principal, null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                    }
                }

                if (request.IsAuthorizationCodeGrantType())
                {
                    var authenticateResult = await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                    var claimsPrincipal = authenticateResult.Principal;
                    return Results.SignIn(claimsPrincipal!, null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                return Results.Problem("The specified grant type is not supported.");
            })
            .ExcludeFromDescription()
            .AllowAnonymous();
}
