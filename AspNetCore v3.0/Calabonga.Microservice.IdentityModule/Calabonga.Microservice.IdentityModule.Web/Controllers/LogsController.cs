using System.Security.Claims;
using AutoMapper;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.Microservice.IdentityModule.Data;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.QueryParams;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.Microservices.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.IdentityModule.Web.Controllers
{
    /// <summary>
    /// ReadOnlyController Demo
    /// </summary>
    [Route("api/[controller]-v1")]
    [Authorize]
    public class LogsController : ReadOnlyController<ApplicationDbContext, ApplicationUser, ApplicationRole, Log, LogViewModel, DefaultPagedListQueryParams>
    {
        /// <inheritdoc />
        public LogsController(IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        [HttpGet("user-roles")]
        public IActionResult Get()
        {
            //Get Roles for current user
            var roles = ClaimsHelper.GetValues<string>((ClaimsIdentity)User.Identity, "role");
            return Ok($"Current user ({User.Identity.Name}) have following roles: {string.Join("|", roles)}");
        }
    }
}
