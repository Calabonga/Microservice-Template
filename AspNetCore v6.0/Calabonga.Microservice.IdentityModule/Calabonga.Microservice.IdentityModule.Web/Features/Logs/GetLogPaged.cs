using AutoMapper;
using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Records;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Features.Logs;

/// <summary>
/// ReadOnlyController Demo
/// </summary>
[Route("api/logs")]
[Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
[Produces("application/json")]
[FeatureGroupName("Logs")]
public class GetLogPagedController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetLogPagedController(IMediator mediator) => _mediator = mediator;
    
    [HttpGet("[action]")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetPaged([FromQuery] PagedListQueryParams queryParams) =>
        Ok(await _mediator.Send(new LogGetPagedRequest(queryParams), HttpContext.RequestAborted));
}

/// <summary>
/// Request for paged list of Logs
/// </summary>
public record LogGetPagedRequest(PagedListQueryParams QueryParams) : OperationResultRequestBase<IPagedList<LogViewModel>>;

/// <summary>
/// Request for paged list of Logs
/// </summary>
public class LogGetPagedRequestHandler : OperationResultRequestHandlerBase<LogGetPagedRequest, IPagedList<LogViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LogGetPagedRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public override async Task<OperationResult<IPagedList<LogViewModel>>> Handle(LogGetPagedRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<IPagedList<LogViewModel>>();

        var pagedList = await _unitOfWork.GetRepository<Log>()
            .GetPagedListAsync(
                pageIndex: request.QueryParams.PageIndex,
                pageSize: request.QueryParams.PageSize,
                cancellationToken: cancellationToken);

        if (pagedList == null)
        {
            operation.Result = PagedList.Empty<LogViewModel>();
            operation.AddWarning("'Repository.GetPagedList' does not return the result for pagination.");
            return operation;
        }

        if (pagedList.PageIndex >= pagedList.TotalPages)
        {
            pagedList = await _unitOfWork.GetRepository<Log>()
                .GetPagedListAsync(
                    pageIndex: 0,
                    pageSize: request.QueryParams.PageSize, cancellationToken: cancellationToken);
        }

        operation.Result = _mapper.Map<IPagedList<LogViewModel>>(pagedList);
        return operation;
    }
}