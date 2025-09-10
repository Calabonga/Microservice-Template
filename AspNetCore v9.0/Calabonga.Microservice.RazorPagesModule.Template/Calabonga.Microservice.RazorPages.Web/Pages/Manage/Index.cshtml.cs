using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calabonga.Microservice.RazorPages.Web.Pages.Manage;

[Authorize]
public class IndexModel : PageModel
{
    public void OnGet()
    {

    }
}
