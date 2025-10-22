namespace IronCore.CORE.Entities;
public class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsArchived { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ArchiveDate { get; set; }
}