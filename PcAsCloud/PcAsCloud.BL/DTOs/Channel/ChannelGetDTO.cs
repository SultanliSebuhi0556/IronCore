namespace PcAsCloud.BL.DTOs.Channel;

public record ChannelGetDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsDirect { get; set; }
    public IEnumerable<string> UserIds { get; set; }
}
