using Microsoft.AspNetCore.Mvc;
using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChannelController(IChannelServices _channelServices) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateChannel([FromQuery] ChannelCreateDTO dto)
    {
        var result = await _channelServices.CreateChannelAsync(dto);
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetChannelById(string id)
    {
        var result = await _channelServices.GetChannelByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllChannels()
    {
        var result = await _channelServices.GetAllChannelsAsync();
        return Ok(result);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteChannel(string id)
    {
        await _channelServices.DeleteChannelAsync(id);
        return Ok();
    }


    [HttpPut("[action]")]
    public async Task<IActionResult> ArchiveUnarchiveChannel(string id)
    {
        await _channelServices.ArchiveUnarchiveChannelAsync(id);
        return Ok();
    }
}
