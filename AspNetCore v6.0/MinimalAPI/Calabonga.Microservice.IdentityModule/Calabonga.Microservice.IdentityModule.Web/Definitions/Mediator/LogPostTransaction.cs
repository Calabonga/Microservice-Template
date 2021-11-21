using Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator.Base;
using Calabonga.Microservice.IdentityModule.Web.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator
{
    public class LogPostTransactionBehavior : TransactionBehavior<IRequest<OperationResult<EventItemViewModel>>, OperationResult<EventItemViewModel>>
    {
        public LogPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
