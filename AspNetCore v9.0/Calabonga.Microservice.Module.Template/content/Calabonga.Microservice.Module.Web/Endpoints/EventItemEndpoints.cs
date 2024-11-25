using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.Module.Domain;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.Queries;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservice.Module.Web.Definitions.Authorizations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.Module.Web.Endpoints;

public sealed class EventItemEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app) => app.MapEventItemEndpoints();
}

internal static class EventItemEndpointsExtensions
{
    public static void MapEventItemEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/event-items/").WithTags(nameof(EventItem));

        group.MapGet("paged/{pageIndex:int}", async ([FromServices] IMediator mediator, int pageIndex, string? search, HttpContext context, int pageSize = 10)
                => await mediator.Send(new GetEventItemPaged.Request(pageIndex, pageSize, search), context.RequestAborted))
            .RequireAuthorization(x => x.AddAuthenticationSchemes(AuthData.AuthSchemes).RequireAuthenticatedUser())
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapGet("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetEventItemById.Request(id), context.RequestAborted))
            .RequireAuthorization(x => x.AddAuthenticationSchemes(AuthData.AuthSchemes).RequireAuthenticatedUser())
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapDelete("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new DeleteEventItem.Request(id), context.RequestAborted))
            .RequireAuthorization(x => x.AddAuthenticationSchemes(AuthData.AuthSchemes).RequireAuthenticatedUser())
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPost("", async ([FromServices] IMediator mediator, EventItemCreateViewModel model, HttpContext context)
                    => await mediator.Send(new PostEventItem.Request(model), context.RequestAborted))
            .RequireAuthorization(x => x.AddAuthenticationSchemes(AuthData.AuthSchemes).RequireAuthenticatedUser())
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPut("{id:guid}", async ([FromServices] IMediator mediator, Guid id, EventItemUpdateViewModel model, HttpContext context)
                    => await mediator.Send(new PutEventItem.Request(id, model), context.RequestAborted))
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();
    }
}