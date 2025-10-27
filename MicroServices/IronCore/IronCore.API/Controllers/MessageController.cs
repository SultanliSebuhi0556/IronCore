using IronCore.API.Features.Commands.MessageCommands.MessageArchive;
using IronCore.API.Features.Commands.MessageCommands.MessageCreate;
using IronCore.API.Features.Commands.MessageCommands.MessageDelete;
using IronCore.API.Features.Queries.MessageQueries.MessageGetById;
using IronCore.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IronCore.API.Controllers;

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

    [HttpGet("[action]")]
    public async Task<IActionResult> Test(string searchString)
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        var client = new HttpClient(handler);
        var url = $"https://localhost:7173/api/Message/GetMessages?searchText={Uri.EscapeDataString(searchString)}";
        var content = await client.GetStringAsync(url);
        return Content(content, "application/json");
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