using $safeprojectname$.Endpoints.ProfileEndpoints.Queries;
using FluentValidation;

namespace $safeprojectname$.Endpoints.ProfileEndpoints;

/// <summary>
/// RegisterViewModel Validator
/// </summary>
public class RegisterRequestValidator : AbstractValidator<RegisterAccountRequest>
{
    public RegisterRequestValidator() => RuleSet("default", () =>
    {
        RuleFor(x => x.Model.Email)
            .NotNull()
                .WithMessage("Электронная почта обязательное поле")
                .WithName("Электронная почта (Email)")
            .EmailAddress().WithMessage("Неверный формат электронной почты");

        RuleFor(x => x.Model.FirstName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Model.LastName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Model.Password).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Model.ConfirmPassword).NotNull().MaximumLength(50).Equal(x => x.Model.Password).When(x => !string.IsNullOrEmpty(x.Model.Password));
    });
}