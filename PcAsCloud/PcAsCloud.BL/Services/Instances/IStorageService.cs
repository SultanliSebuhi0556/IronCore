using Microsoft.AspNetCore.Http;

namespace PcAsCloud.BL.Services.Services.Instances;

public interface IStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string? newFileName, CancellationToken cancellationToken);
    Task<MemoryStream> GetFileAsync(string fileName, CancellationToken cancellationToken);
    Task DeleteFileAsync(string fileName);
}
