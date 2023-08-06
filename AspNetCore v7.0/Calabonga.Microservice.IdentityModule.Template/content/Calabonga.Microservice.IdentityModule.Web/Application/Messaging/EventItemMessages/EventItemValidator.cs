using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemsMessages.Queries;
using FluentValidation;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemsMessages;

/// <summary>
/// RegisterViewModel Validator
/// </summary>
public class EventItemCreateRequestValidator : AbstractValidator<PostEventItem.Request>
{
    public EventItemCreateRequestValidator() => RuleSet("default", () =>
    {
        RuleFor(x => x.Model.CreatedAt).NotNull();
        RuleFor(x => x.Model.Message).NotEmpty().MaximumLength(4000);
        RuleFor(x => x.Model.Level).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Model.Logger).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Model.ThreadId).MaximumLength(50);
        RuleFor(x => x.Model.ExceptionMessage).MaximumLength(4000);
    });
}