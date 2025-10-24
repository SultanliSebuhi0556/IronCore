namespace SearchService.CORE.Entities;
public class Message : BaseEntity
{
    public const string IndexName = "messages";
    public string? Content { get; set; }
    public bool IsRead { get; set; } = false;
    public string ChannelId { get; set; }
    public string? StorageId { get; set; }
    public string SendedById { get; set; }
}