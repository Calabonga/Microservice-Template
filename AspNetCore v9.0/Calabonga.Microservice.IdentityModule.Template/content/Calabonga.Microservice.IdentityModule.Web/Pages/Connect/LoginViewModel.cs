using System.ComponentModel.DataAnnotations;

namespace Calabonga.Microservice.IdentityModule.Web.Pages.Connect;

public class LoginViewModel
{

    [Required]
    [EmailAddress]
    [Display(Name = "User name")]
    public string UserName { get; set; } = null!;

    [Required]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    [Required]
    public string? ReturnUrl { get; set; } = null!;
}