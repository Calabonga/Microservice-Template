using System.Collections.Generic;
using Calabonga.AspNetCore.MicroModule.Data;
using Calabonga.AspNetCore.MicroModule.Web.Controllers.Base;
using Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Services;
using Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Settings;
using Calabonga.EntityFrameworkCore.UOW;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Calabonga.AspNetCore.MicroModule.Web.Controllers
{
    /// <summary>
    /// Demo purposes only
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : UnitOfWorkController
    {
        /// <inheritdoc />
        public ValuesController(IOptions<CurrentAppSettings> options, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(options, unitOfWork)
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
            return OperationResultInfo("MicroService", "Information");
        }
    }
}
