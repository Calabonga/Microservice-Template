using Calabonga.Microservice.IdentityModule.Data;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Features.Authentication
{
    public class Login
    {

    }


    [Produces("application/json")]
    [AllowAnonymous]
    [SwaggerGroup("Authentication")]
    [Route("api/authentication")]
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public IActionResult Login(string returnUrl) => View("../Features/Authentication/Login");

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please. Validate your credentials and try again.");
                return View("../Features/Authentication/Login", model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View("../Features/Authentication/Login", model);
            }

            var signResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!signResult.Succeeded)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View("../Features/Authentication/Login", model);
            }

            return Redirect(model.ReturnUrl);
        }


    }
}
