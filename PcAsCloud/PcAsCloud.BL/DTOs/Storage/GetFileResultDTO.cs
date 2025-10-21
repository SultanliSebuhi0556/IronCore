namespace PcAsCloud.BL.DTOs.Storage;
public record GetFileResultDTO
{
    public MemoryStream Stream { get; set; }
    public string FileName { get; set; }
}