using $safeprojectname$.Definitions.Identity;
using $safeprojectname$.Endpoints.Base;
using $safeprojectname$.Endpoints.EventItemsEndpoints.Queries;
using $safeprojectname$.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Endpoints.EventItemsEndpoints
{
    public class EventItemEndpoints : EndpointDefinition
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
            => await mediator.Send(new GetEventItemByIdRequest(id), context.RequestAborted);

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
        [FeatureGroupName("EventItems")]
        private async Task<OperationResult<EventItemViewModel>> LogDelete([FromServices] IMediator mediator, Guid id, HttpContext context)
            => await mediator.Send(new DeleteEventItemRequest(id), context.RequestAborted);

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
        [FeatureGroupName("EventItems")]
        private async Task<OperationResult<IPagedList<EventItemViewModel>>> LogGetPaged([FromServices] IMediator mediator, int pageIndex, int pageSize, string? search, HttpContext context)
            => await mediator.Send(new GetEventItemPagedRequest(pageIndex, pageSize, search), context.RequestAborted);

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
        [FeatureGroupName("EventItems")]
        private async Task<OperationResult<EventItemViewModel>> PostLog([FromServices] IMediator mediator, EventItemCreateViewModel model, HttpContext context)
            => await mediator.Send(new PostEventItemRequest(model), context.RequestAborted);

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
        [FeatureGroupName("EventItems")]
        private async Task<OperationResult<EventItemViewModel>> PutLog([FromServices] IMediator mediator, Guid id, EventItemUpdateViewModel model, HttpContext context)
            => await mediator.Send(new PutEventItemRequest(id, model), context.RequestAborted);
    }
}