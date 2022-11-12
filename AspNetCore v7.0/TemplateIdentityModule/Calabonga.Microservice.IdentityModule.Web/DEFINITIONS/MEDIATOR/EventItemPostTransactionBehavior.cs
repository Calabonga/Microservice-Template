using $safeprojectname$.Definitions.Mediator.Base;
using $safeprojectname$.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace $safeprojectname$.Definitions.Mediator;

public class EventItemPostTransactionBehavior : TransactionBehavior<IRequest<OperationResult<EventItemViewModel>>, OperationResult<EventItemViewModel>>
{
    public EventItemPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
}