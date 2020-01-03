using System;
using System.Threading.Tasks;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.Infrastructure.ViewModels.AccountViewModels;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResultsCore;
using Calabonga.UnitOfWork.Controllers.Controllers;
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
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
        public async Task<ActionResult<OperationResult<UserProfileViewModel>>> Register(RegisterViewModel model)
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
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
        public async Task<ActionResult<OperationResult<UserProfileViewModel>>> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return OperationResultError<UserProfileViewModel>(null, new MicroserviceUnauthorizedException());
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