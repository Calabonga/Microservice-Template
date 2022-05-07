using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Calabonga.AuthService.Infrastructure;

/// <summary>
/// Application store for user
/// </summary>
public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, Guid>
{
    public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
    {
    }
}