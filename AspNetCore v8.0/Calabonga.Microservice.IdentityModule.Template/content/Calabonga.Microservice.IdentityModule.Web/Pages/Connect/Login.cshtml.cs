using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calabonga.Microservice.IdentityModule.Web.Pages.Connect;

[AllowAnonymous]
public class LoginModel(
        IAccountService accountService,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    : PageModel
{
    [BindProperty(SupportsGet = true)] public string ReturnUrl { get; set; } = null!;

    [BindProperty] public LoginViewModel? Input { get; set; }

    public void OnGet() => Input = new LoginViewModel
    {
        ReturnUrl = ReturnUrl
    };

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Input != null)
        {
            var user = await userManager.FindByNameAsync(Input.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "Пользователь не найден");
                return Page();
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, Input.Password, true, false);
            if (signInResult.Succeeded)
            {
                var principal = await accountService.GetPrincipalByIdAsync(user.Id.ToString());
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToPage("/swagger");
            }
        }

        ModelState.AddModelError("UserName", "Пользователь не найден");
        return Page();
    }
}