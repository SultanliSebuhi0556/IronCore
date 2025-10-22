namespace IronCore.BL.DTOs.Storage;
public record GetFileDTO
{
    public string StorageId { get; set; }
    public string? ChannelId { get; set; }
}
