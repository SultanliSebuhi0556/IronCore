using Microsoft.AspNetCore.Http;

namespace IronCore.BL.DTOs.Message;
public record MessageCreateDTO
{
    public string ChannelId { get; set; }
    public string? Content { get; set; }
    public IFormFile? File { get; set; }
}