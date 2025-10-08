namespace PcAsCloud.CORE.Entities;
public class Channel : BaseEntity
{
    public string Name { get; set; }
    public bool IsDirect { get; set; }
    public IEnumerable<AppUser> Users { get; set; }
    public IEnumerable<Message> Messages { get; set; }
}