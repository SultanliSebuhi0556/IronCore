namespace IronCore.BL.DTOs.Storage;
public record UploadFileResultDTO
{
    public Guid StorageId { get; set; }
    public string FileName { get; set; }
}