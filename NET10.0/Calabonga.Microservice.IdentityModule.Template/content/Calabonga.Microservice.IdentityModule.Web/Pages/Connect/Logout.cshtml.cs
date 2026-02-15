using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenIddict.Client.AspNetCore;
using OpenIddict.Server.AspNetCore;

namespace Calabonga.Microservice.IdentityModule.Web.Pages.Connect;

public class LogoutModel : PageModel
{
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(string returnUrl)
    {
        // Retrieve the identity stored in the local authentication cookie. If it's not available,
        // this indicate that the user is already logged out locally (or has not logged in yet).
        //
        // For scenarios where the default authentication handler configured in the ASP.NET Core
        // authentication options shouldn't be used, a specific scheme can be specified here.
        var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        if (result is not { Succeeded: true })
        {
            // Only allow local return URLs to prevent open redirect attacks.
            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }

        // Remove the local authentication cookie before triggering a redirection to the remote server.
        //
        // For scenarios where the default sign-out handler configured in the ASP.NET Core
        // authentication options shouldn't be used, a specific scheme can be specified here.
        await HttpContext.SignOutAsync();

        var properties = new AuthenticationProperties(new Dictionary<string, string>
        {
            // While not required, the specification encourages sending an id_token_hint
            // parameter containing an identity token returned by the server for this user.
            [OpenIddictClientAspNetCoreConstants.Properties.IdentityTokenHint] = result.Properties.GetTokenValue(OpenIddictClientAspNetCoreConstants.Tokens.BackchannelIdentityToken)
        })
        {
            // Only allow local return URLs to prevent open redirect attacks.
            RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
        };

        // Ask the OpenIddict client middleware to redirect the user agent to the identity provider.
        return SignOut(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

    }
}
