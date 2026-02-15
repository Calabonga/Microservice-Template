using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calabonga.AspNetCoreRazorPages.Web.Pages;

[Authorize]
public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var user = HttpContext.User;

        _logger.LogInformation("{UserName} - {IsAuthenticated}", user.Identity?.Name, user.Identity?.IsAuthenticated);
    }
}

