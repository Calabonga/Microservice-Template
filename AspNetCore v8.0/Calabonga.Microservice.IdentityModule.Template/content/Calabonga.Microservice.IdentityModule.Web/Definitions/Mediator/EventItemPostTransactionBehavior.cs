using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator.Base;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator;

public class EventItemPostTransactionBehavior(IUnitOfWork unitOfWork)
    : TransactionBehavior<IRequest<Operation<EventItemViewModel>>, Operation<EventItemViewModel>>(unitOfWork);