using Microsoft.AspNetCore.Mvc;
using PcAsCloud.BL.DTOs.Message;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController(IMessageService _messageService, IWebHostEnvironment _webHostEnvironment) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateMessage([FromQuery] MessageCreateDTO dto)
    {
        var result = await _messageService.CreateMessageAsync(dto, _webHostEnvironment.WebRootPath);
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetMessageById(string id)
    {
        var result = await _messageService.GetMessageByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllMessagesByChannelId(string id)
    {
        var result = await _messageService.GetAllMessagesByChannelIdAsync(id);
        return Ok(result);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteMessage(string id)
    {
        await _messageService.DeleteMessageAsync(id);
        return Ok();
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> ArchiveUnarchiveMessage(string id)
    {
        await _messageService.ArchiveUnarchiveMessageAsync(id);
        return Ok();
    }
}