using Calabonga.Microservice.IdentityModule.Domain.Base;
using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Application.Services;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Base;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.AuthEndpoints;

/// <summary>
/// Token Endpoint for OpenIddict
/// </summary>
public class TokenEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment) =>
        app.MapPost("~/connect/token", TokenAsync).ExcludeFromDescription();

    private async Task<IResult> TokenAsync(HttpContext httpContext, IOpenIddictScopeManager manager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        var request = httpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");


        AuthenticationProperties? properties = null;
        if (request.IsClientCredentialsGrantType())
        {
            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            // Subject or sub is a required field, we use the client id as the subject identifier here.
            identity.AddClaim(OpenIddictConstants.Claims.Subject, request.ClientId!);
            identity.AddClaim(OpenIddictConstants.Claims.ClientId, request.ClientId!);

            // Don't forget to add destination otherwise it won't be added to the access token.
            identity.AddClaim(OpenIddictConstants.Claims.Scope, request.Scope!, OpenIddictConstants.Destinations.AccessToken);
            identity.AddClaim("nimble", "framework", OpenIddictConstants.Destinations.AccessToken);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            claimsPrincipal.SetScopes(request.GetScopes());
            return Results.SignIn(claimsPrincipal, properties ?? new AuthenticationProperties(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }


        if (request.IsPasswordGrantType())
        {
            var user = await userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // Ensure the user is allowed to sign in
            if (!await signInManager.CanSignInAsync(user))
            {
                throw new InvalidOperationException();
            }


            // Ensure the user is not already locked out
            if (userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user))
            {
                throw new InvalidOperationException();
            }

            // Ensure the password is valid
            if (!await userManager.CheckPasswordAsync(user, request.Password))
            {
                if (userManager.SupportsUserLockout)
                {
                    await userManager.AccessFailedAsync(user);
                }

                throw new InvalidOperationException();
            }

            // Reset the lockout count
            if (userManager.SupportsUserLockout)
            {
                await userManager.ResetAccessFailedCountAsync(user);
            }

            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);


            if (user.ApplicationUserProfile?.Permissions != null)
            {
                identity.AddClaims(user.ApplicationUserProfile.Permissions.Select(permission =>
                    new Claim(
                        ClaimTypes.Role,
                        permission.PolicyName,
                        OpenIddictConstants.Destinations.AccessToken, 
                        AppData.ServiceName)));
            }

            // Look up the user's roles (if any)
            var roles = Array.Empty<string>();
            if (userManager.SupportsUserRole)
            {
                roles = (await userManager.GetRolesAsync(user)).ToArray();
            }

            if (roles.Any())
            {
                identity
                    .AddClaims(roles
                        .Select(role => new Claim(
                            ClaimTypes.Role,
                            role,
                            OpenIddictConstants.Destinations.AccessToken,
                            OpenIddictConstants.Destinations.IdentityToken))
                        .ToList());
            }

            // Subject or sub is a required field, we use the client id as the subject identifier here.
            identity.AddClaim(OpenIddictConstants.Claims.Subject, request.Username!);
            identity.AddClaim(ClaimTypes.Email, user.Email);
            identity.AddClaim(ClaimTypes.MobilePhone, user.PhoneNumber);
            identity.AddClaim(ClaimTypes.Name, $"{user.LastName} {user.FirstName}", OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);

            var claimsPrincipal = new ClaimsPrincipal(identity);
            return Results.SignIn(claimsPrincipal, properties ?? new AuthenticationProperties(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        if (request.IsAuthorizationCodeGrantType())
        {
            var authenticateResult = await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            properties = authenticateResult.Properties;
            var claimsPrincipal = authenticateResult.Principal;
            return Results.SignIn(claimsPrincipal!, properties ?? new AuthenticationProperties(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }
}