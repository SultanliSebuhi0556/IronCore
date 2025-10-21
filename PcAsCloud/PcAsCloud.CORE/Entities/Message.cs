namespace PcAsCloud.CORE.Entities;
public class Message : BaseEntity
{
    public string? Content { get; set; }
    public bool IsRead { get; set; } = false;

    public Channel Channel { get; set; }
    public Guid ChannelId { get; set; }

    public Storage? Storage { get; set; }
    public Guid? StorageId { get; set; }

    public AppUser SendedBy { get; set; }
    public string SendedById { get; set; }
}