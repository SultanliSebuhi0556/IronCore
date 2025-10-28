using Microsoft.AspNetCore.Mvc;
using SearchService.BL.Services.Instances;

namespace SearchService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController(IMessageElasticService _messageService) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetMessages(string channelId, string searchText)
    {
        CancellationToken cancellationToken = new CancellationToken();
        var messages = await _messageService.GetAllBySearchAsync(channelId, searchText, cancellationToken);
        return Ok(messages);
    }
}