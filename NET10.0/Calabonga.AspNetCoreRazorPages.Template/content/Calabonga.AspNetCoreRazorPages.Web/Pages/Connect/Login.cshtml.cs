using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenIddict.Client.AspNetCore;

namespace Calabonga.AspNetCoreRazorPages.Web.Pages.Connect;

public class LoginModel : PageModel
{
    public ActionResult OnGet(string returnUrl)
    {
        var properties = new AuthenticationProperties
        {
            // Only allow local return URLs to prevent open redirect attacks.
            RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
        };

        // Ask the OpenIddict client middleware to redirect the user agent to the identity provider.
        return Challenge(properties, OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
    }
}
