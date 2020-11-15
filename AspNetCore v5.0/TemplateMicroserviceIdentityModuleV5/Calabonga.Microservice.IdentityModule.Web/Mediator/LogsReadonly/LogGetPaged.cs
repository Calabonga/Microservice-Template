using AutoMapper;
using Calabonga.AspNetCore.Controllers.Handlers;
using Calabonga.AspNetCore.Controllers.Queries;
using $ext_projectname$.Entities;
using $safeprojectname$.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;

namespace $safeprojectname$.Mediator.LogsReadonly
{
    /// <summary>
    /// Request for paged list of Logs
    /// </summary>
    public class LogGetPagedRequest : GetPagedQuery<LogViewModel, PagedListQueryParams>
    {
        public LogGetPagedRequest(PagedListQueryParams queryParams) : base(queryParams)
        {
        }
    }

    /// <summary>
    /// Request for paged list of Logs
    /// </summary>
    public class LogGetPagedRequestHandler : GetPagedHandlerBase<LogGetPagedRequest, Log, PagedListQueryParams, LogViewModel>
    {
        public LogGetPagedRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
