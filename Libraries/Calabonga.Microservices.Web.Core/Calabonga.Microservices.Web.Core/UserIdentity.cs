using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calabonga.Microservices.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Calabonga.Microservices.Web.Core
{
    /// <summary>
    /// Identity helper for Requests operations
    /// </summary>
    public sealed class UserIdentity
    {
        private UserIdentity() { }

        public static UserIdentity Instance => Lazy.Value;

        public void Configure(IHttpContextAccessor httpContextAccessor)
        {
            IsInitialized = true;
            ContextAccessor = httpContextAccessor;
        }

        public IIdentity User
        {
            get
            {
                if (IsInitialized)
                {
                    return ContextAccessor.HttpContext.User.Identity.IsAuthenticated
                        ? ContextAccessor.HttpContext.User.Identity
                        : null;
                }
                throw new MicroserviceArgumentNullException($"{nameof(HttpContext)} has not been initialized");
            }
        }

        private static readonly Lazy<UserIdentity> Lazy = new Lazy<UserIdentity>(() => new UserIdentity());

        private bool IsInitialized { get; set; }

        private static IHttpContextAccessor ContextAccessor { get; set; }

    }
}
