using Calabonga.Microservice.IdentityModule.Web.Application.Services;
using Calabonga.Microservice.IdentityModule.Web.Endpoints.ProfileEndpoints.ViewModels;
using Calabonga.OperationResults;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.ProfileEndpoints.Queries;

/// <summary>
/// Register new account
/// </summary>
public class RegisterAccount
{
    public class Request : IRequest<OperationResult<UserProfileViewModel>>
    {
        public Request(RegisterViewModel model) => Model = model;

        public RegisterViewModel Model { get; }
    }

    public class Handler : IRequestHandler<Request, OperationResult<UserProfileViewModel>>
    {
        private readonly IAccountService _accountService;

        public Handler(IAccountService accountService)
            => _accountService = accountService;

        public Task<OperationResult<UserProfileViewModel>> Handle(Request request, CancellationToken cancellationToken)
            => _accountService.RegisterAsync(request.Model, cancellationToken);
    }
}