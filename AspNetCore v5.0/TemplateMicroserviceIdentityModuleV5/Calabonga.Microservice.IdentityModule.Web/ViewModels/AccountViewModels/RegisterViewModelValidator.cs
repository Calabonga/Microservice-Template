using $safeprojectname$.Mediator.Account;
using FluentValidation;

namespace $safeprojectname$.ViewModels.AccountViewModels
{
    /// <summary>
    /// RegisterViewModel Validator
    /// </summary>
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleSet("default", () =>
            {
                RuleFor(x => x.Model.Email).NotNull().EmailAddress();
                RuleFor(x => x.Model.FirstName).NotEmpty().NotNull().MaximumLength(50);
                RuleFor(x => x.Model.LastName).NotEmpty().NotNull().MaximumLength(50);
                RuleFor(x => x.Model.Password).NotNull().NotEmpty().MaximumLength(50);
                RuleFor(x => x.Model.ConfirmPassword).NotNull().MaximumLength(50).Equal(x => x.Model.Password).When(x => !string.IsNullOrEmpty(x.Model.Password));
            });

        }
    }
}