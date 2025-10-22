namespace IronCore.CORE.Entities;
public class Storage
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string FileName { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}