namespace IronCore.BL.DTOs.Channel;

public record ChannelCreateDTO
{
    public bool IsDirect { get; set; }
    public string? ChannelName { get; set; }
    public string? TargetUserId { get; set; }
}