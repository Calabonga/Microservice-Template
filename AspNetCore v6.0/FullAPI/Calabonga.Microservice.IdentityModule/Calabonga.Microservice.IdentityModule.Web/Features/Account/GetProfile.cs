using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth;
using Calabonga.OperationResults;
using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Features.Account
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [Produces("application/json")]
    [Route("api/account")]
    [FeatureGroupName("Account")]
    public class GetProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Register controller
        /// </summary>
        public GetProfileController(IMediator mediator) => _mediator = mediator;


        /// <summary>
        /// Returns profile information for authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
        public async Task<ActionResult<OperationResult<UserProfileViewModel>>> GetProfile() =>
            await _mediator.Send(new ProfileRequest(), HttpContext.RequestAborted);
    }

    /// <summary>
    /// Request: Profile
    /// </summary>
    public class ProfileRequest : RequestBase<OperationResult<UserProfileViewModel>> { }

    /// <summary>
    /// Response: Profile
    /// </summary>
    public class ProfileRequestHandler : OperationResultRequestHandlerBase<ProfileRequest, UserProfileViewModel>
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileRequestHandler(
            IAccountService accountService,
            IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<OperationResult<UserProfileViewModel>> Handle(ProfileRequest request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null)
            {
                return await _accountService.GetProfileByIdAsync(user.Identity.GetSubjectId());
            }

            var operation = OperationResult.CreateResult<UserProfileViewModel>();
            operation.AddWarning("User not found");
            return operation;
        }
    }
}