using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Calabonga.AspNetCore.Micro.Data;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.Auth
{
    /// <summary>
    /// Custom Extension Grant Validator
    /// </summary>
    public class SchedulerValidator : IExtensionGrantValidator
    {
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;
        private readonly ApplicationClaimsPrincipalFactory _claimsFactory;


        /// <inheritdoc />
        public SchedulerValidator(
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork,
            ApplicationClaimsPrincipalFactory claimsFactory)
        {
            _unitOfWork = unitOfWork;
            _claimsFactory = claimsFactory;
        }

        /// <inheritdoc />
        public string GrantType => "scheduler";

        /// <inheritdoc />
        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var accessCode = context.Request.Client.ClientId;
            var clientCode = context.Request.Raw.Get(accessCode);
            if (string.IsNullOrWhiteSpace(clientCode))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid api access code credentials");
                return Task.CompletedTask;
            }

            var guid = Guid.Parse("12c75df8-2ca5-63a8-4314-b49a10aaabad");
            if (guid != Guid.Parse(clientCode))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, " Invalid api access code credentials");
                return Task.CompletedTask;
            }

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, accessCode),
                new Claim(JwtClaimTypes.IdentityProvider, "local"),
                new Claim(ClaimTypes.AuthenticationMethod, "scheduler"),
                new Claim(JwtClaimTypes.AuthenticationTime, DateTime.Now.Ticks.ToString()),
                new Claim(JwtClaimTypes.Scope, "api1"),
                new Claim(JwtClaimTypes.Scope, "api2"),
                new Claim("amr", "pwd")
            };
            var identity = new ClaimsIdentity(claims, "scheduler");
            var principal = new ClaimsPrincipal(identity);
            context.Result = new GrantValidationResult(principal);
            return Task.CompletedTask;
        }
    }
}