using Microsoft.AspNetCore.Http;

namespace PcAsCloud.BL.ExternalServices.Storage;

public class SaveFileService() : ISaveFileService
{
    public async Task<string> SaveFileAsync(string path, IFormFile file, string fileName)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string fullPath = Path.Combine(path, fileName + Path.GetExtension(file.FileName));

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fullPath;
    }
}