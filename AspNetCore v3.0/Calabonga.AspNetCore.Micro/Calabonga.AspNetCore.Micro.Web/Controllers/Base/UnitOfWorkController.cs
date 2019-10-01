using System;
using Calabonga.AspNetCore.Micro.Core.Exceptions;
using Calabonga.AspNetCore.Micro.Data;
using Calabonga.AspNetCore.Micro.Entities;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Services;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Settings;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Calabonga.AspNetCore.Micro.Web.Controllers.Base
{
    /// <summary>
    /// Unit Of Work controller
    /// </summary>
    [Route("api/[controller]")]
    public abstract class UnitOfWorkController : OperationResultController
    {
        /// <inheritdoc />
        protected UnitOfWorkController(IOptions<CurrentAppSettings> options,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork,
            IAccountService accountService)
        {
            AccountService = accountService;
            CurrentSettings = options.Value;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Allows to save data logs to the database Logs table
        /// </summary>
        protected void LogInformation(string message, Exception exception = null)
        {
            var logs = UnitOfWork.GetRepository<Log>();
            var log = new Log
            {
                CreatedAt = DateTime.UtcNow,
                Level = LogLevel.Information.ToString(),
                Logger = GetType().Name,
                Message = message,
                ThreadId = "0",
                ExceptionMessage = exception?.Message
            };
            logs.Insert(log);
            UnitOfWork.SaveChanges();
            if (!UnitOfWork.LastSaveChangesResult.IsOk)
            {
                throw new MicroserviceInvalidOperationException();
            }
        }
        
        /// <summary>
        /// Allows to save data logs to the database Logs table
        /// </summary>
        private void Log(LogLevel level, string message, Exception exception = null)
        {
            var logs = UnitOfWork.GetRepository<Log>();
            var log = new Log
            {
                CreatedAt = DateTime.UtcNow,
                Level = level.ToString(),
                Logger = GetType().Name,
                Message = message,
                ThreadId = "0",
                ExceptionMessage = exception?.Message
            };
            logs.Insert(log);
            UnitOfWork.SaveChanges();
            if (!UnitOfWork.LastSaveChangesResult.IsOk)
            {
                throw new MicroserviceInvalidOperationException();
            }
        }

        /// <summary>
        /// Account service instance
        /// </summary>
        protected IAccountService AccountService { get; }

        /// <summary>
        /// Current Unit Of Work 
        /// </summary>
        protected IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> UnitOfWork { get; set; }

        /// <summary>
        /// Current application settings
        /// </summary>
        protected CurrentAppSettings CurrentSettings { get; }
    }
}