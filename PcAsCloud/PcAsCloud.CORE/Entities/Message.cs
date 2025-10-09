namespace PcAsCloud.CORE.Entities;
public class Message : BaseEntity
{
    public string? Content { get; set; }
    public string? FileUrl { get; set; }

    public Channel Channel { get; set; }
    public Guid ChannelId { get; set; }

    public AppUser SendedBy { get; set; }
    public string SendedById { get; set; }
}