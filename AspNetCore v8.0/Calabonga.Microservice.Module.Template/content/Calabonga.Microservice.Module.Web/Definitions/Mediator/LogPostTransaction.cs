using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservice.Module.Web.Definitions.Mediator.Base;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.Module.Web.Definitions.Mediator;

public class LogPostTransactionBehavior : TransactionBehavior<IRequest<Operation<EventItemViewModel>>, Operation<EventItemViewModel>>
{
    public LogPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
}