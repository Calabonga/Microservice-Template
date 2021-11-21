using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Base;
using $ext_projectname$.Data;
using $safeprojectname$.Infrastructure.Attributes;
using $safeprojectname$.Infrastructure.Mappers.Base;
using Calabonga.Microservices.Core;
using Calabonga.OperationResults;
using FluentValidation;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Account;

/// <summary>
/// Account Controller
/// </summary>
[Produces("application/json")]
[Route("api/account")]
[FeatureGroupName("Account")]
public class CreateUserController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Register controller
    /// </summary>
    public CreateUserController(IMediator mediator) => _mediator = mediator;


    /// <summary>
    /// Register new user. Success registration returns UserProfile for new user.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("[action]")]
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
    public async Task<ActionResult<OperationResult<UserProfileViewModel>>> CreateUser([FromBody] RegisterViewModel model) =>
        Ok(await _mediator.Send(new RegisterRequest(model), HttpContext.RequestAborted));

}

/// <summary>
/// Request: Register new account
/// </summary>
public class RegisterRequest : RequestBase<OperationResult<UserProfileViewModel>>
{
    public RegisterViewModel Model { get; }

    public RegisterRequest(RegisterViewModel model) => Model = model;
}

/// <summary>
/// RegisterViewModel Validator
/// </summary>
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator() => RuleSet("default", () =>
    {
        RuleFor(x => x.Model.Email).NotNull().EmailAddress();
        RuleFor(x => x.Model.FirstName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Model.LastName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Model.Password).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Model.ConfirmPassword).NotNull().MaximumLength(50).Equal(x => x.Model.Password).When(x => !string.IsNullOrEmpty(x.Model.Password));
    });
}

/// <summary>
/// Response: Register new account
/// </summary>
public class RegisterRequestHandler : OperationResultRequestHandlerBase<RegisterRequest, UserProfileViewModel>
{
    private readonly IAccountService _accountService;

    public RegisterRequestHandler(IAccountService accountService) => _accountService = accountService;

    public override Task<OperationResult<UserProfileViewModel>> Handle(RegisterRequest request, CancellationToken cancellationToken) => _accountService.RegisterAsync(request.Model);
}

/// <summary>
/// Data transfer object for user registration
/// </summary>
public class RegisterViewModel
{
    /// <summary>
    /// FirstName
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    /// <summary>
    /// LastName
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    /// <summary>
    /// Password confirmation
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}


/// <summary>
/// Mapper Configuration for entity Person
/// </summary>
public class ApplicationUserProfileMapperConfiguration : MapperConfigurationBase
{
    /// <inheritdoc />
    public ApplicationUserProfileMapperConfiguration() => CreateMap<RegisterViewModel, ApplicationUserProfile>().ForAllOtherMembers(x => x.Ignore());
}

/// <summary>
/// Mapper Configuration for entity ApplicationUser
/// </summary>
public class UserMapperConfiguration : MapperConfigurationBase
{
    /// <inheritdoc />
    public UserMapperConfiguration()
    {
        CreateMap<RegisterViewModel, ApplicationUser>()
            .ForMember(x => x.UserName, o => o.MapFrom(p => p.Email))
            .ForMember(x => x.Email, o => o.MapFrom(p => p.Email))
            .ForMember(x => x.EmailConfirmed, o => o.MapFrom(src => true))
            .ForMember(x => x.FirstName, o => o.MapFrom(p => p.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(p => p.LastName))
            .ForMember(x => x.PhoneNumberConfirmed, o => o.MapFrom(src => true))
            .ForAllOtherMembers(x => x.Ignore());

        CreateMap<ClaimsIdentity, UserProfileViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(claims => ClaimsHelper.GetValue<Guid>(claims, JwtClaimTypes.Subject)))
            .ForMember(x => x.PositionName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Actor)))
            .ForMember(x => x.FirstName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, JwtClaimTypes.GivenName)))
            .ForMember(x => x.LastName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Surname)))
            .ForMember(x => x.Roles, o => o.MapFrom(claims => ClaimsHelper.GetValues<string>(claims, JwtClaimTypes.Role)))
            .ForMember(x => x.Email, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, JwtClaimTypes.Name)))
            .ForMember(x => x.PhoneNumber, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, JwtClaimTypes.PhoneNumber)))
            .ForAllOtherMembers(x => x.Ignore());
    }
}