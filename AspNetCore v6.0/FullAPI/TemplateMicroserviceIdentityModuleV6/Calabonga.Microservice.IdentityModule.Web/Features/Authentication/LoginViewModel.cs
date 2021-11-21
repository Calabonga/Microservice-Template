using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Features.Authentication;

public class LoginViewModel
{
    [Required] public string UserName { get; set; } = null!;
        
    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string ReturnUrl { get; set; } = null!;
}