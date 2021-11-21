using AutoMapper;
using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Records;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Features.Logs
{
    /// <summary>
    /// Get logs by identifier
    /// </summary>
    [Route("api/logs")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [Produces("application/json")]
    [FeatureGroupName("Logs")]
    public class GetLogByIdController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetLogByIdController(IMediator mediator) => _mediator = mediator;

        [HttpGet("[action]/{id:guid}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _mediator.Send(new LogGetByIdRequest(id), HttpContext.RequestAborted));
    }

    /// <summary>
    /// Request for Log by Identifier
    /// </summary>
    public record LogGetByIdRequest(Guid Id) : OperationResultRequestBase<LogViewModel>;

    /// <summary>
    /// Response for  Request for Log by Identifier
    /// </summary>
    public class LogGetByIdRequestHandler : OperationResultRequestHandlerBase<LogGetByIdRequest, LogViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LogGetByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<OperationResult<LogViewModel>> Handle(LogGetByIdRequest request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var operation = OperationResult.CreateResult<LogViewModel>();
            var repository = _unitOfWork.GetRepository<Log>();
            var entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entityWithoutIncludes == null)
            {
                operation.AddError(new MicroserviceNotFoundException($"Entity with identifier {id} not found"));
                return operation;
            }
            operation.Result = _mapper.Map<LogViewModel>(entityWithoutIncludes);
            return operation;
        }
    }
}