using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$;

/// <summary>
/// Application store for user
/// </summary>
public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>
{
    public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer)
        : base(context, describer)
    {

    }

    /// <summary>
    /// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
    /// </summary>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.
    /// </returns>
    public override Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return Users
            .Include(x => x.ApplicationUserProfile)
            .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId, cancellationToken: cancellationToken)!;
    }
}