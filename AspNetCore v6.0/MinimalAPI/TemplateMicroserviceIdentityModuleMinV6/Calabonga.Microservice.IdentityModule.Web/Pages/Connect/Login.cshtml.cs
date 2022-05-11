using $ext_projectname$.Infrastructure;
using $safeprojectname$.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace $safeprojectname$.Pages.Connect;

public class LoginModel : PageModel
{
    private readonly IAccountService _accountService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginModel(
        IAccountService accountService,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _accountService = accountService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; }

    [BindProperty]
    public LoginViewModel Input { get; set; }

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

        var user = await _userManager.FindByNameAsync(Input.UserName);
        if (user == null)
        {
            ModelState.AddModelError("UserName", "Пользователь не найден");
            return Page();
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, Input.Password, true, false);
        if (signInResult.Succeeded)
        {
            var principal = await _accountService.GetPrincipalByIdAsync(user.Id.ToString());
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                
            if (Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToPage("/swagger");
        }

        ModelState.AddModelError("UserName", "Пользователь не найден");
        return Page();
    }
}