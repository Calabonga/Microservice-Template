using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.QueryParams;
using Calabonga.Microservice.IdentityModule.Data;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Settings;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Calabonga.Microservice.IdentityModule.Web.Controllers
{
    /// <summary>
    /// WritableController Demo
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class LogsWritableController : WritableController<ApplicationDbContext, ApplicationUser, ApplicationRole, Log, LogCreateViewModel, LogUpdateViewModel, LogViewModel, PagedListQueryParams>
    {
        private readonly CurrentAppSettings _appSettings;

        /// <inheritdoc />
        public LogsWritableController(
            IOptions<CurrentAppSettings> appSettings,
            IEntityManager<Log, LogCreateViewModel, LogUpdateViewModel> entityManager,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
            : base(entityManager, unitOfWork)
        {
            _appSettings = appSettings.Value;
        }

        /// <inheritdoc />
        protected override PermissionValidationResult ValidateQueryParams(PagedListQueryParams queryParams)
        {
            if (queryParams.PageSize <= 0)
            {
                queryParams.PageSize = _appSettings.PageSize;
            }
            return base.ValidateQueryParams(queryParams);
        }
    }
}