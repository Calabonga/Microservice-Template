using System.Threading;
using System.Threading.Tasks;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Handlers
{
    /// <summary>
    /// GetCreateViewModelHandler Handler
    /// </summary>
    public abstract class CreateViewModelHandlerBase<TRequest, TResponse> : OperationResultRequestHandlerBase<TRequest, TResponse> 
        where TRequest : IRequest<OperationResult<TResponse>>
        where TResponse : IViewModel, new()
    {
        protected virtual ValueTask<TResponse> GenerateCreateViewModel() => new ValueTask<TResponse>(new TResponse());

        public override async Task<OperationResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<TResponse>();
            var model = await GenerateCreateViewModel();
            operation.Result = model;
            operation.AddSuccess("CreateViewModel successfully generated");
            return operation;
        }
    }
}
