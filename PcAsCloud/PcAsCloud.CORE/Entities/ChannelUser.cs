namespace PcAsCloud.CORE.Entities;

public class ChannelUser
{
    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
}
