using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemsMessages.ViewModels;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator.Base;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator;

public class EventItemPostTransactionBehavior : TransactionBehavior<IRequest<OperationResult<EventItemViewModel>>, OperationResult<EventItemViewModel>>
{
    public EventItemPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
}