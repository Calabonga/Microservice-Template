using Calabonga.AspNetCore.Controllers;
using System.Threading;
using System.Threading.Tasks;

using Calabonga.AspNetCore.Controllers.Base;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.ViewModels.AccountViewModels;
using Calabonga.OperationResults;

namespace $safeprojectname$.Mediator.Account
{
    /// <summary>
    /// Request: Register new account
    /// </summary>
    public class RegisterRequest : RequestBase<OperationResult<UserProfileViewModel>>
    {
        public RegisterViewModel Model { get; }

        public RegisterRequest(RegisterViewModel model) => Model = model;
    }

    /// <summary>
    /// Response: Register new account
    /// </summary>
    public class RegisterRequestHandler : OperationResultRequestHandlerBase<RegisterRequest, UserProfileViewModel>
    {
        private readonly IAccountService _accountService;

        public RegisterRequestHandler(IAccountService accountService) => _accountService = accountService;

        public override Task<OperationResult<UserProfileViewModel>> Handle(RegisterRequest request, CancellationToken cancellationToken) => _accountService.RegisterAsync(request.Model);
    }
}