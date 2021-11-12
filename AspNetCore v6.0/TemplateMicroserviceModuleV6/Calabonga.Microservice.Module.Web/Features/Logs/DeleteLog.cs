using AutoMapper;
using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Records;
using $ext_projectname$.Entities;
using $safeprojectname$.Infrastructure.Attributes;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Logs;

/// <summary>
/// WritableController Demo
/// </summary>
[Route("api/logs")]
[Authorize]
[Produces("application/json")]
[FeatureGroupName("Logs")]
public class DeleteLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteLogController(IMediator mediator) => _mediator = mediator;

    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id) =>
        Ok(await _mediator.Send(new LogDeleteItemRequest(id), HttpContext.RequestAborted));
}


/// <summary>
/// Request: Log delete
/// </summary>
public record LogDeleteItemRequest(Guid Id) : OperationResultRequestBase<LogViewModel>;

/// <summary>
/// Request: Log delete
/// </summary>
public class LogDeleteItemRequestHandler : OperationResultRequestHandlerBase<LogDeleteItemRequest, LogViewModel>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public LogDeleteItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public override async Task<OperationResult<LogViewModel>> Handle(LogDeleteItemRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<LogViewModel>();
        var repository = _unitOfWork.GetRepository<Log>();
        var entity = await repository.FindAsync(request.Id);
        if (entity == null)
        {
            operation.AddError(new MicroserviceNotFoundException("Entity not found"));
            return operation;
        }
        repository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();
        if (_unitOfWork.LastSaveChangesResult.IsOk)
        {
            operation.Result = _mapper.Map<LogViewModel>(entity);
            return operation;
        }
        operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
        return operation;
    }
}