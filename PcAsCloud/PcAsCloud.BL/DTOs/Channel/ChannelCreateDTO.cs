using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.DTOs.Channel;

public record ChannelCreateDTO
{
    public AppUser CurrentUser { get; set; }
    public bool IsDirect { get; set; }
    public string? ChannelName { get; set; }
    public AppUser? TargertUser { get; set; }
}