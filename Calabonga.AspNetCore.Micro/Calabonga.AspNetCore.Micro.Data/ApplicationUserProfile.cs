using System;
using Calabonga.AspNetCore.Micro.Models.Base;

namespace Calabonga.AspNetCore.Micro.Data
{
    /// <summary>
    /// Represent person with login information (ApplicationUser)
    /// </summary>
    public class ApplicationUserProfile: Auditable
    {
        /// <summary>
        /// Account <see cref="Data.ApplicationUser"/>
        /// </summary>
        public Guid ApplicationUserId { get; set; }

        /// <summary>
        /// Account
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
