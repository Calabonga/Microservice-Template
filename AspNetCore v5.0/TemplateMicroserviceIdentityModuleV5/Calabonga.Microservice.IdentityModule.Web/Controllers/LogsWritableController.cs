using System;
using System.Threading.Tasks;
using $safeprojectname$.Infrastructure.Auth;
using $safeprojectname$.Mediator.LogsReadonly;
using $safeprojectname$.Mediator.LogsWritable;
using $safeprojectname$.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.QueryParams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Controllers
{
    /// <summary>
    /// WritableController Demo
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    public class LogsWritableController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsWritableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet("[action]/{id:guid}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new LogGetByIdRequest(id), HttpContext.RequestAborted));
        }


        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPaged([FromQuery] PagedListQueryParams queryParams)
        {
            return Ok(await _mediator.Send(new LogGetPagedRequest(queryParams), HttpContext.RequestAborted));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetViewmodelForCreation()
        {
            return Ok(await _mediator.Send(new LogCreateViewModelRequest(), HttpContext.RequestAborted));
        }
        
        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> GetViewmodelForEditing(Guid id)
        {
            return Ok(await _mediator.Send(new LogUpdateViewModelRequest(id), HttpContext.RequestAborted));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostItem([FromBody]LogCreateViewModel model)
        {
            return Ok(await _mediator.Send(new LogPostItemRequest(model), HttpContext.RequestAborted));
        }
        
        [HttpPut("[action]")]
        public async Task<IActionResult> PutItem([FromBody]LogUpdateViewModel model)
        {
            return Ok(await _mediator.Send(new LogPutItemRequest(model), HttpContext.RequestAborted));
        }
        
        [HttpDelete("[action]/{id:guid}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            return Ok(await _mediator.Send(new LogDeleteItemRequest(id), HttpContext.RequestAborted));
        }
    }
}