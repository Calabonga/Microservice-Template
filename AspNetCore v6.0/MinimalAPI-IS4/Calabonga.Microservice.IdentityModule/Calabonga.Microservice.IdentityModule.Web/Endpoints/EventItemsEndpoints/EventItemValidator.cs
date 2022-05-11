using Calabonga.Microservice.IdentityModule.Web.Endpoints.EventItemsEndpoints.ViewModels;
using FluentValidation;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.EventItemsEndpoints
{
    /// <summary>
    /// RegisterViewModel Validator
    /// </summary>
    public class EventItemCreateRequestValidator : AbstractValidator<EventItemCreateViewModel>
    {
        public EventItemCreateRequestValidator() => RuleSet("default", () =>
        {
            RuleFor(x => x.CreatedAt).NotNull();
            RuleFor(x => x.Message).NotEmpty().NotNull().MaximumLength(4000);
            RuleFor(x => x.Level).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Logger).NotNull().NotEmpty().MaximumLength(255);

            RuleFor(x => x.ThreadId).MaximumLength(50);
            RuleFor(x => x.ExceptionMessage).MaximumLength(4000);
        });
    }
}