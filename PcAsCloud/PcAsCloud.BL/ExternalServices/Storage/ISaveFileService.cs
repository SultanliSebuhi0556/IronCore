using Microsoft.AspNetCore.Http;

namespace PcAsCloud.BL.ExternalServices.Storage;
public interface ISaveFileService
{
    Task<string> SaveFileAsync(string path, IFormFile file, string fileName);
}