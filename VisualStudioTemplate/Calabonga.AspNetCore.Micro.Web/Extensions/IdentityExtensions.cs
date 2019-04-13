using System;
using System.Security.Principal;
using Calabonga.AspNetCore.Micro.Core;
using IdentityServer4.Extensions;

namespace $safeprojectname$.Extensions
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Returns true/false user is owner
        /// </summary>
        /// <param name="source"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool IsOwner(this IIdentity source, Guid userId)
        {
            return source.GetSubjectId().ToGuid() == userId;
        }
    }
}
