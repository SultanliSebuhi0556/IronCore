using IronCore.API.Features.Commands.ChannelCommands.ChannelArchive;
using IronCore.API.Features.Commands.ChannelCommands.ChannelCreate;
using IronCore.API.Features.Commands.ChannelCommands.ChannelDelete;
using IronCore.API.Features.Commands.ChannelCommands.ChannelJoin;
using IronCore.API.Features.Commands.ChannelCommands.ChannelLeave;
using IronCore.API.Features.Queries.ChannelQueries.ChannelGetAll;
using IronCore.API.Features.Queries.ChannelQueries.ChannelGetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IronCore.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ChannelController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateChannel([FromQuery] ChannelCreateRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetChannelById([FromQuery] ChannelGetByIdRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllChannels([FromQuery] ChannelGetAllRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteChannel([FromQuery] ChannelDeleteRequest request)
    {
        return Ok(await _mediator.Send(request));
    }


    [HttpPut("[action]")]
    public async Task<IActionResult> ArchiveUnarchiveChannel([FromQuery] ChannelArchiveRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> JoinChannel([FromQuery] ChannelJoinRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> LeaveChannel([FromQuery] ChannelLeaveRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}
