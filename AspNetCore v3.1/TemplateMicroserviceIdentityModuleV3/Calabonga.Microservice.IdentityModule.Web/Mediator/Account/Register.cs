using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Calabonga.AspNetCore.Controllers.Base;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.ViewModels.AccountViewModels;
using Calabonga.OperationResultsCore;

namespace $safeprojectname$.Mediator.Account
{
    /// <summary>
    /// Request: Register new account
    /// </summary>
    public class RegisterRequest : RequestBase<OperationResult<UserProfileViewModel>>
    {
        public RegisterViewModel Model { get; }

        public RegisterRequest(RegisterViewModel model)
        {
            Model = model;
        }
    }

    /// <summary>
    /// Response: Register new account
    /// </summary>
    public class RegisterRequestHandler : OperationResultRequestHandlerBase<RegisterRequest, UserProfileViewModel>
    {
        private readonly IAccountService _accountService;
        
        public RegisterRequestHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public override Task<OperationResult<UserProfileViewModel>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var registerViewModelValidator = new RegisterViewModelValidator();
            var result = registerViewModelValidator.Validate(request.Model);
            if (result.IsValid)
            {
                return _accountService.RegisterAsync(request.Model);
            }

            var operation = OperationResult.CreateResult<UserProfileViewModel>();
            operation.AppendLog(result.Errors.Select(x => x.ErrorMessage));
            return Task.FromResult(operation);
        }
    }
}