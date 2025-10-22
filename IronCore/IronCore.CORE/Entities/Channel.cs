namespace IronCore.CORE.Entities;
public class Channel : BaseEntity
{
    public string Name { get; set; }
    public bool IsDirect { get; set; }
    public ICollection<ChannelUser> ChannelUsers { get; set; }
    public ICollection<Message> Messages { get; set; }
}