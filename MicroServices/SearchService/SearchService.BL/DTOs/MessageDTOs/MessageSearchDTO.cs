namespace SearchService.BL.DTOs.MessageDTOs;
public record MessageSearchDTO
{
    public string Id { get; set; }
    public string? Content { get; set; }
    public string? StorageId { get; set; }
    public bool IsRead { get; set; }
    public string SendedById { get; set; }
}