namespace PcAsCloud.BL.DTOs.Message;
public record MessageCreateResponseDTO
{
    public string Id { get; set; }
    public string? StorageId { get; set; }
    public string? FileName { get; set; }
}