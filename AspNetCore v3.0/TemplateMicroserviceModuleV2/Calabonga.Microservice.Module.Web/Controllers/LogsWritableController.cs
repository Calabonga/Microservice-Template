using System.Threading.Tasks;
using Calabonga.EntityFrameworkCore.UOW;
using Calabonga.EntityFrameworkCore.UOW.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UOW.Framework.Managers;
using Calabonga.EntityFrameworkCore.UOW.Framework.QueryParams;
using $ext_projectname$.Data;
using $ext_projectname$.Entities;
using $safeprojectname$.Infrastructure.Settings;
using $safeprojectname$.Infrastructure.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.Validators;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace $safeprojectname$.Controllers
{
    /// <summary>
    /// WritableController Demo
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class LogsWritableController : WritableController<ApplicationDbContext, Log, LogCreateViewModel, LogUpdateViewModel, LogViewModel, PagedListQueryParams>
    {
        private readonly CurrentAppSettings _appSettings;

        /// <inheritdoc />
        public LogsWritableController(
            IOptions<CurrentAppSettings> appSettings,
            IEntityManager<Log, LogCreateViewModel, LogUpdateViewModel> entityManager,
            IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(entityManager, unitOfWork)
        {
            _appSettings = appSettings.Value;
        }

        /// <inheritdoc />
        [Authorize(Policy = "LogsWritable:GetCreateViewModelAsync:View")]
        public override Task<ActionResult<OperationResult<LogCreateViewModel>>> GetCreateViewModelAsync()
        {
            return base.GetCreateViewModelAsync();
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