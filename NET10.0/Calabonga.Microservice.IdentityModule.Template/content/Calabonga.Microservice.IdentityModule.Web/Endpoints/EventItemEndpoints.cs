using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Domain.Base;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.Queries;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints;

public sealed class EventItemEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        var group = app.MapGroup("/api/event-items/").WithTags(nameof(EventItem));

        group.MapGet("paged/{pageIndex:int}", async ([FromServices] IMediator mediator, string? search, HttpContext context, int pageIndex = 0, int pageSize = 10)
                => await mediator.Send(new GetEventItemPaged.Request(pageIndex, pageSize, search), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404);

        group.MapGet("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetEventItemById.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404);

        group.MapDelete("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new DeleteEventItem.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404);

        group.MapPost("", async ([FromServices] IMediator mediator, EventItemCreateViewModel model, HttpContext context)
                => await mediator.Send(new PostEventItem.Request(model), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404);

        group.MapPut("{id:guid}", async ([FromServices] IMediator mediator, Guid id, EventItemUpdateViewModel model, HttpContext context)
                => await mediator.Send(new PutEventItem.Request(id, model), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404);
    }
}
