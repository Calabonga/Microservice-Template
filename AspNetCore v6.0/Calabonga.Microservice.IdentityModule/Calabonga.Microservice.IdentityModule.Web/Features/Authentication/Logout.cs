using Calabonga.Microservice.IdentityModule.Data;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Features.Authentication;

[Produces("application/json")]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
[FeatureGroupName("Authentication")]
[Route("api/authentication")]
public class LogoutController : Controller
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogoutController(
        IIdentityServerInteractionService interaction,
        SignInManager<ApplicationUser> signInManager)
    {
        _interaction = interaction;
        _signInManager = signInManager;
    }

    [HttpGet("[action]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Logout(string logoutId)
    {
        var logout = await _interaction.GetLogoutContextAsync(logoutId);
        await _signInManager.SignOutAsync();
        return Redirect(logout.PostLogoutRedirectUri);
    }
}