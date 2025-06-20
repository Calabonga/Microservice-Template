﻿using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservice.Module.Web.Definitions.Mediator.Base;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Mediator;

namespace Calabonga.Microservice.Module.Web.Definitions.Mediator;

public class LogPostTransactionBehavior : TransactionBehavior<IRequest<Operation<EventItemViewModel>>, Operation<EventItemViewModel>>
{
    public LogPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
}
