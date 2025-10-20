using Microsoft.AspNetCore.Http;

namespace PcAsCloud.BL.ExternalServices.Instances;

public interface IFileHelper
{
    Task<string> SaveFileAsync(string folderName, string fileName, Stream? stream, IFormFile? file, CancellationToken cancellationToken = default);
    Task<MemoryStream> GetFileAsync(string folderName, string fileName, CancellationToken cancellationToken);
    Task DeleteFileAsync(string folderName, string fileName);
}
