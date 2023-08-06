using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.Queries;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservice.Module.Web.Definitions.Identity;
using Calabonga.Microservices.Core;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.Module.Web.Endpoints;

public class EventItemEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
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
    [FeatureGroupName("EventItems")]
    private async Task<OperationResult<EventItemViewModel>> LogGetById([FromServices] IMediator mediator, Guid id, HttpContext context)
        => await mediator.Send(new GetEventItemById.Request(id), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("EventItems")]
    private async Task<OperationResult<EventItemViewModel>> LogDelete([FromServices] IMediator mediator, Guid id, HttpContext context)
        => await mediator.Send(new DeleteEventItem.Request(id), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("EventItems")]
    private async Task<OperationResult<IPagedList<EventItemViewModel>>> LogGetPaged([FromServices] IMediator mediator, int pageIndex, int pageSize, string? search, HttpContext context)
        => await mediator.Send(new GetEventItemPaged.Request(pageIndex, pageSize, search), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("EventItems")]
    private async Task<OperationResult<EventItemViewModel>> PostLog([FromServices] IMediator mediator, EventItemCreateViewModel model, HttpContext context)
        => await mediator.Send(new PostEventItem.Request(model), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("EventItems")]
    private async Task<OperationResult<EventItemViewModel>> PutLog([FromServices] IMediator mediator, Guid id, EventItemUpdateViewModel model, HttpContext context)
        => await mediator.Send(new PutEventItem.Request(id, model), context.RequestAborted);
}