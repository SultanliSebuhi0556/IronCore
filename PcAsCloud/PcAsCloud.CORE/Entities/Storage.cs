namespace PcAsCloud.CORE.Entities;
public class Storage
{
    public Guid Id { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public string? ChannelId { get; set; }
    public Channel? Channel { get; set; }

    public string FileName { get; set; }
}