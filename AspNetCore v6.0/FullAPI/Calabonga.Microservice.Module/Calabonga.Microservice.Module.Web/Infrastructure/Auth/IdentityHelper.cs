using System;
using System.Security.Principal;
using Calabonga.Microservices.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Auth
{
    /// <summary>
    /// Identity helper for Requests operations (Singleton)
    /// </summary>
    public sealed class IdentityHelper
    {
        private IdentityHelper() { }

        public static IdentityHelper Instance => Lazy.Value;

        public void Configure(IHttpContextAccessor httpContextAccessor)
        {
            ContextAccessor = httpContextAccessor ?? throw new MicroserviceArgumentNullException(nameof(IHttpContextAccessor));
            IsInitialized = true;
        }

        public IIdentity? User
        {
            get
            {
                if (IsInitialized)
                {
                    return ContextAccessor.HttpContext!.User.Identity != null 
                           && ContextAccessor.HttpContext != null 
                           && ContextAccessor.HttpContext.User.Identity.IsAuthenticated
                        ? ContextAccessor.HttpContext.User.Identity
                        : null;
                }
                throw new MicroserviceArgumentNullException($"{nameof(IdentityHelper)} has not been initialized. Please use {nameof(IdentityHelper)}.Instance.Configure(...) in Configure Application method in Startup.cs");
            }
        }

        private static readonly Lazy<IdentityHelper> Lazy = new Lazy<IdentityHelper>(() => new IdentityHelper());

        private bool IsInitialized { get; set; }

        private static IHttpContextAccessor ContextAccessor { get; set; } = null!;

    }
}
