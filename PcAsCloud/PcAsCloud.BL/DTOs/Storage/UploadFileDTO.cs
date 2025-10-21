using Microsoft.AspNetCore.Http;

namespace PcAsCloud.BL.DTOs.Storage;

public record UploadFileDTO
{
    public IFormFile File { get; set; }
    public string? NewFolderName { get; set; }
    public string? NewFileName { get; set; }
}