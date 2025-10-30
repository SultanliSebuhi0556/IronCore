namespace SharedDTOs.DTOs;
public record MessageCreateDTO
{
    public string Id { get; set; }
    public string? Content { get; set; }
    public bool IsRead { get; set; } = false;
    public string ChannelId { get; set; }
    public string? StorageId { get; set; }
    public string SendedById { get; set; }
}