namespace IronCore.BL.DTOs.Storage;
public record DeleteFileDTO
{
    public string StorageId { get; set; }
    public string? ChannelId { get; set; }
}