using $safeprojectname$.Definitions.Identity;
using $safeprojectname$.Endpoints.Base;
using $safeprojectname$.Endpoints.LogsEndpoints.Queries;
using $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Endpoints.LogsEndpoints;

public class LogsEndpoints : IEndpoint
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }

    public void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/logs/{id}", LogGetById);
        app.MapGet("/api/logs/paged", LogGetPaged);
        app.MapPost("/api/logs/", PostLog);
        app.MapPut("/api/logs/{id}", PutLog);
        app.MapDelete("/api/logs/{id}", LogDelete);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("Logs")]
    private async Task<OperationResult<LogViewModel>> LogGetById([FromServices] IMediator mediator, Guid id, HttpContext context) 
        => await mediator.Send(new LogGetByIdRequest(id), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("Logs")]
    private async Task<OperationResult<LogViewModel>> LogDelete([FromServices] IMediator mediator, Guid id, HttpContext context)
        => await mediator.Send(new LogDeleteItemRequest(id), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("Logs")]
    private async Task<OperationResult<IPagedList<LogViewModel>>> LogGetPaged([FromServices] IMediator mediator, int pageIndex, int pageSize, string? search, HttpContext context)
        => await mediator.Send(new LogGetPagedRequest(pageIndex, pageSize, search), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("Logs")]
    private async Task<OperationResult<LogViewModel>> PostLog([FromServices] IMediator mediator, LogCreateViewModel model, HttpContext context) 
        => await mediator.Send(new LogPostRequest(model), context.RequestAborted);
    
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("Logs")]
    private async Task<OperationResult<LogViewModel>> PutLog([FromServices] IMediator mediator, Guid id, LogUpdateViewModel model, HttpContext context) 
        => await mediator.Send(new LogPutRequest(id, model), context.RequestAborted);
}