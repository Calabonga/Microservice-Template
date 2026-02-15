using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages.ViewModels;
using Calabonga.Microservice.IdentityModule.Web.Application.Services;
using Calabonga.OperationResults;
using Mediator;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages.Queries;

/// <summary>
/// Register new account
/// </summary>
public static class RegisterAccount
{
    public class Request(RegisterViewModel model) : IRequest<Operation<UserProfileViewModel, string>>
    {
        public RegisterViewModel Model { get; } = model;
    }

    public class Handler(IAccountService accountService)
        : IRequestHandler<Request, Operation<UserProfileViewModel, string>>
    {
        public ValueTask<Operation<UserProfileViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
            => accountService.RegisterAsync(request.Model, cancellationToken);
    }
}
