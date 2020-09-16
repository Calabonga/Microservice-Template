using FluentValidation;

namespace Calabonga.Microservice.IdentityModule.Web.ViewModels.AccountViewModels
{
    /// <summary>
    /// RegisterViewModel Validator
    /// </summary>
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleSet("default", () =>
            {
                RuleFor(x => x.Email).NotNull().EmailAddress();
                RuleFor(x => x.FirstName).NotNull().MaximumLength(50);
                RuleFor(x => x.LastName).NotNull().MaximumLength(50);
                RuleFor(x => x.Password).NotNull().MaximumLength(50);
                RuleFor(x => x.ConfirmPassword).NotNull().MaximumLength(50).Equal(x => x.Password).When(x => !string.IsNullOrEmpty(x.Password));
            });

        }
    }
}