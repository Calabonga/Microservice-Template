using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.Queries;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints;

public sealed class EventItemEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/logs/paged", async ([FromServices] IMediator mediator, int pageIndex, int pageSize, string? search, HttpContext context)
                => await mediator.Send(new GetEventItemPaged.Request(pageIndex, pageSize, search), context.RequestAborted))
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithTags("Logs")
            .WithOpenApi();

        app.MapGet("/api/logs/{id}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetEventItemById.Request(id), context.RequestAborted))
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithTags("Logs")
            .WithOpenApi();

        app.MapDelete("/api/logs/{id}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new DeleteEventItem.Request(id), context.RequestAborted))
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithTags("Logs")
            .WithOpenApi();

        app.MapPost("/api/logs/", async ([FromServices] IMediator mediator, EventItemCreateViewModel model, HttpContext context)
                    => await mediator.Send(new PostEventItem.Request(model), context.RequestAborted))
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithTags("Logs")
            .WithOpenApi();

        app.MapPut("/api/logs/{id}", async ([FromServices] IMediator mediator, Guid id, EventItemUpdateViewModel model, HttpContext context)
                    => await mediator.Send(new PutEventItem.Request(id, model), context.RequestAborted))
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithTags("Logs")
            .WithOpenApi();
    }
}