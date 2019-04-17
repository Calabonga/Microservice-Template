using System.Collections.Generic;
using $ext_safeprojectname$.Data;
using $safeprojectname$.Controllers.Base;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.Infrastructure.Settings;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace $safeprojectname$.Controllers
{
    /// <summary>
    /// Demo purposes only
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : UnitOfWorkController
    {
        /// <inheritdoc />
        public ValuesController(IOptions<CurrentAppSettings> options, IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork, IAccountService accountService) 
            : base(options, unitOfWork, accountService)
        {
        }

        /// <summary>
        /// Returns a collection.
        /// Demo purposes only
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<OperationResult<IEnumerable<string>>> Get()
        {
            var items = new string[] { "Calabonga", "Template", "For", "Micro", "Services" };
            return OperationResultSuccess<IEnumerable<string>>(items);
        }

        /// <summary>
        /// Return something by identifier
        /// Demo purposes only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public ActionResult<OperationResult<string>> Get(int id)
        {
            return OperationResultInfo("MicroService","Information");
        }
    }
}
