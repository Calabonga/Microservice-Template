using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$
{
    /// <summary>
    /// Application store for user
    /// </summary>
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
            
        }

        /// <inheritdoc />
        public override Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken = new CancellationToken())
        {
            return Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId, cancellationToken: cancellationToken);
        }
    }
}