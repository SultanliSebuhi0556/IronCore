namespace PcAsCloud.BL.DTOs.Message;
public record MessageCreateResponseDTO
{
    public string Id { get; set; }
    public string? FilePath { get; set; }
}