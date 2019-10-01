using System.Collections.Generic;
using Calabonga.AspNetCore.Micro.Data;
using Calabonga.AspNetCore.Micro.Web.Controllers.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Services;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Settings;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Calabonga.AspNetCore.Micro.Web.Controllers
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

        [HttpGet("user-data")]
        public IActionResult Get()
        {
            return Ok(User.Identity.Name);
        }
    }
}
