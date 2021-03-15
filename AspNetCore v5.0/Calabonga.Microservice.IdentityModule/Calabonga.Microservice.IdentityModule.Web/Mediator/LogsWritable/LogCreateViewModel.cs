using Calabonga.Microservice.IdentityModule.Web.Mediator.Handlers;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Queries;
using System;
using System.Threading.Tasks;
using Calabonga.Microservice.IdentityModule.Web.ViewModels.LogViewModels;
using Microsoft.Extensions.Logging;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.LogsWritable
{
    /// <summary>
    /// Request: Returns ViewModel for entity Log creation
    /// </summary>
    public class LogCreateViewModelRequest: CreateViewModelQuery<LogCreateViewModel>
    {
        
    }

    /// <summary>
    /// Response: Returns ViewModel for entity Log creation
    /// </summary>
    public class LogCreateViewModelRequestHandler : CreateViewModelHandlerBase<LogCreateViewModelRequest, LogCreateViewModel>
    {
        protected override ValueTask<LogCreateViewModel> GenerateCreateViewModel() => new ValueTask<LogCreateViewModel>(new LogCreateViewModel
        {
            CreatedAt = DateTime.UtcNow,
            Level = LogLevel.Information.ToString(),
            Message = "Generated automatically",
            Logger = "LogCreateViewModelRequestHandler",
            ThreadId = "0"
        });
    }
}
