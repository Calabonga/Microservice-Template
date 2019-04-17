using System;
using System.Threading.Tasks;
using $ext_safeprojectname$.Core;
using $safeprojectname$.Controllers.Base;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.Infrastructure.ViewModels.AccountViewModels;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : OperationResultController
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Register controller
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Register new user. Success registration returns UserProfile for new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(OperationResult<ApplicationUserProfileViewModel>))]
        public async Task<ActionResult<OperationResult<ApplicationUserProfileViewModel>>> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return OperationResultResponse(await _accountService.RegisterAsync(model));
        }

        /// <summary>
        /// Returns profile information for authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        [ProducesResponseType(200, Type = typeof(OperationResult<ApplicationUserProfileViewModel>))]
        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return OperationResultError(AppData.Messages.AccessDenied);
            }

            var userId = _accountService.GetCurrentUserId();
            if (Guid.Empty == userId)
            {
                return BadRequest();
            }

            var claimsOperationResult = await _accountService.GetProfileAsync(userId.ToString());
            return Ok(claimsOperationResult);
        }
    }
}
