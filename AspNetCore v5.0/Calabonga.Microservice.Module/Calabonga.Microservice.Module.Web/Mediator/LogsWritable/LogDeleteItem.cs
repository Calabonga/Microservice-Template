using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.Microservice.Module.Entities;
using Calabonga.Microservice.Module.Web.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.Module.Web.Mediator.LogsWritable
{
    /// <summary>
    /// Request: Log delete
    /// </summary>
    public class LogDeleteItemRequest : OperationResultRequestBase<LogViewModel>
    {
        public LogDeleteItemRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    /// <summary>
    /// Request: Log delete
    /// </summary>
    public class LogDeleteItemRequestHandler : OperationResultRequestHandlerBase<LogDeleteItemRequest, LogViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public LogDeleteItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<OperationResult<LogViewModel>> Handle(LogDeleteItemRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<LogViewModel>();
            var repository = _unitOfWork.GetRepository<Log>();
            var entity = await repository.FindAsync(request.Id);
            if (entity == null)
            {
                operation.AddError(new MicroserviceNotFoundException("Entity not found"));
                return operation;
            }
            repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.Result = _mapper.Map<LogViewModel>(entity);
                return operation;
            }
            operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
            return operation;
        }
    }
}
