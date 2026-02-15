using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calabonga.AspNetCoreRazorPages.Web.Pages.Manage;

[Authorize]
public class IndexModel : PageModel
{
    public void OnGet()
    {

    }
}
