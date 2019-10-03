using System.Security.Claims;
using AutoMapper;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.QueryParams;
using Calabonga.Microservice.IdentityModule.Data;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Settings;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Calabonga.Microservice.IdentityModule.Web.Controllers
{
    /// <summary>
    /// ReadOnlyController Demo
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class LogsReadOnlyController : ReadOnlyController<ApplicationDbContext, ApplicationUser, ApplicationRole, Log, LogViewModel, PagedListQueryParams>
    {
        private readonly CurrentAppSettings _appSettings;

        /// <inheritdoc />
        public LogsReadOnlyController(
            IOptions<CurrentAppSettings> appSettings,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork, 
            IMapper mapper)
            : base(unitOfWork, mapper)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet("user-roles")]
        [Authorize(Policy = "Logs:UserRoles:View")]
        public IActionResult Get()
        {
            //Get Roles for current user
            var roles = ClaimsHelper.GetValues<string>((ClaimsIdentity)User.Identity, "role");
            return Ok($"Current user ({User.Identity.Name}) have following roles: {string.Join("|", roles)}");
        }

        /// <inheritdoc />
        protected override PermissionValidationResult ValidateQueryParams(PagedListQueryParams queryParams)
        {
            if (queryParams.PageSize <= 0)
            {
                queryParams.PageSize = _appSettings.PageSize;
            }
            return new PermissionValidationResult();
        }
    }
}
