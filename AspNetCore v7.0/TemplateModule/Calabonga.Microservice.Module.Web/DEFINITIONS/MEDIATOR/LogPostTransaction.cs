using $safeprojectname$.Definitions.Mediator.Base;
using $safeprojectname$.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace $safeprojectname$.Definitions.Mediator;

public class LogPostTransactionBehavior : TransactionBehavior<IRequest<OperationResult<EventItemViewModel>>, OperationResult<EventItemViewModel>>
{
    public LogPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
}