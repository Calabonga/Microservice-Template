using System;
using System.Security.Principal;
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
            ContextAccessor = httpContextAccessor ?? throw new MicroserviceArgumentNullException(nameof(IHttpContextAccessor));
            IsInitialized = true;
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
                throw new MicroserviceArgumentNullException($"{nameof(UserIdentity)} has not been initialized. Please use {nameof(UserIdentity)}.Configure(...) in Configure method in Startup.cs");
            }
        }

        private static readonly Lazy<UserIdentity> Lazy = new Lazy<UserIdentity>(() => new UserIdentity());

        private bool IsInitialized { get; set; }

        private static IHttpContextAccessor ContextAccessor { get; set; }

    }
}
