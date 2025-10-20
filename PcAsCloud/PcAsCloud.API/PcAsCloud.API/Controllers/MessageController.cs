using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcAsCloud.API.Features.Commands.MessageCommands.MessageArchive;
using PcAsCloud.API.Features.Commands.MessageCommands.MessageCreate;
using PcAsCloud.API.Features.Commands.MessageCommands.MessageDelete;
using PcAsCloud.API.Features.Queries.MessageQueries.MessageGetById;
using PcAsCloud.API.Features.Queries.MessageQueries.MessageGetChannelMessages;

namespace PcAsCloud.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MessageController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateMessage([FromQuery] MessageCreateRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetMessageById([FromQuery] MessageGetByIdRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllMessagesByChannelId([FromQuery] MessageGetChannelMessagesRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteMessage([FromQuery] MessageDeleteRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> ArchiveUnarchiveMessage([FromQuery] MessageArchiveRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}