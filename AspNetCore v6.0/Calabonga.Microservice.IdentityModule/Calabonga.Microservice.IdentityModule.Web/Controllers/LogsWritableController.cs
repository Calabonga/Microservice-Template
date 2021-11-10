using System;
using System.Threading.Tasks;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth;
using Calabonga.Microservice.IdentityModule.Web.Mediator.LogsReadonly;
using Calabonga.Microservice.IdentityModule.Web.Mediator.LogsWritable;
using Calabonga.Microservice.IdentityModule.Web.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.QueryParams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.IdentityModule.Web.Controllers;

/// <summary>
/// WritableController Demo
/// </summary>
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
public class LogsWritableController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogsWritableController(IMediator mediator) => _mediator = mediator;


    [HttpGet("[action]/{id:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(await _mediator.Send(new LogGetByIdRequest(id), HttpContext.RequestAborted));


    [HttpGet("[action]")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetPaged([FromQuery] PagedListQueryParams queryParams) =>
        Ok(await _mediator.Send(new LogGetPagedRequest(queryParams), HttpContext.RequestAborted));
        
    [HttpPost("[action]")]
    public async Task<IActionResult> PostItem([FromBody]LogCreateViewModel model) => 
        Ok(await _mediator.Send(new LogPostItemRequest(model), HttpContext.RequestAborted));

    [HttpPut("[action]")]
    public async Task<IActionResult> PutItem([FromBody]LogUpdateViewModel model) =>
        Ok(await _mediator.Send(new LogPutItemRequest(model), HttpContext.RequestAborted));

    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id) =>
        Ok(await _mediator.Send(new LogDeleteItemRequest(id), HttpContext.RequestAborted));
}