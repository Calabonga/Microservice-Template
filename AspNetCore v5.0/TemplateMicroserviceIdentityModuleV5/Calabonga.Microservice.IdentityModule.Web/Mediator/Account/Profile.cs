using Calabonga.AspNetCore.Controllers;
using System.Threading;
using System.Threading.Tasks;

using Calabonga.AspNetCore.Controllers.Base;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.ViewModels.AccountViewModels;
using Calabonga.OperationResults;

using IdentityServer4.Extensions;

using Microsoft.AspNetCore.Http;

namespace $safeprojectname$.Mediator.Account
{
    /// <summary>
    /// Request: Profile
    /// </summary>
    public class ProfileRequest : RequestBase<OperationResult<UserProfileViewModel>>
    {
    }

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