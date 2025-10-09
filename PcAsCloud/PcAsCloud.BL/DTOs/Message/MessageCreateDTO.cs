using Microsoft.AspNetCore.Http;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.DTOs.Message;
public record MessageCreateDTO
{
    public AppUser CurrentUser { get; set; }
    public string ChannelId { get; set; }
    public string RootPath { get; set; }
    public string? Content { get; set; }
    public IFormFile? File { get; set; }
}