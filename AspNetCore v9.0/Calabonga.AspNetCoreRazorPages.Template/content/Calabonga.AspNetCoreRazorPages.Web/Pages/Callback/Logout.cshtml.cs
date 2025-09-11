using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenIddict.Client.AspNetCore;

namespace Calabonga.AspNetCoreRazorPages.Web.Pages.Callback;

[ValidateAntiForgeryToken]
public class LogoutModel : PageModel
{
    public async Task<ActionResult> OnGetAsync(string returnUrl)
    {
        return await LogOutCallback();
    }


    public async Task<ActionResult> OnPostAsync(string returnUrl)
    {
        return await LogOutCallback();
    }

    public async Task<ActionResult> LogOutCallback()
    {
        // Retrieve the data stored by OpenIddict in the state token created when the logout was triggered.
        var result = await HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);

        // In this sample, the local authentication cookie is always removed before the user agent is redirected
        // to the authorization server. Applications that prefer delaying the removal of the local cookie can
        // remove the corresponding code from the logout action and remove the authentication cookie in this action.

        var redirectUri = result!.Properties!.RedirectUri;
        if (redirectUri != null)
        {
            return Redirect(redirectUri);
        }

        return Redirect("~/Index");
    }
}
