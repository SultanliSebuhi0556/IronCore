using PcAsCloud.BL.DTOs.Storage;

namespace PcAsCloud.BL.Services.Services.Instances;

public interface IStorageService
{
    Task<string> SaveFileAsync(UploadFileDTO dto, CancellationToken cancellationToken);
    Task<MemoryStream> GetFileAsync(string fileName, CancellationToken cancellationToken);
    Task DeleteFileAsync(string fileName);
}
