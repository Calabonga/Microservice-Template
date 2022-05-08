using Calabonga.AuthService.Web.Definitions.Mediator.Base;
using Calabonga.AuthService.Web.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.AuthService.Web.Definitions.Mediator;

public class EventItemPostTransactionBehavior : TransactionBehavior<IRequest<OperationResult<EventItemViewModel>>, OperationResult<EventItemViewModel>>
{
    public EventItemPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
}