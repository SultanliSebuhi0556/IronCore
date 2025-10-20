using Microsoft.AspNetCore.Http;
using PcAsCloud.BL.ExternalServices.Instances;

namespace PcAsCloud.BL.ExternalServices.Implements;
public class FileHelper : IFileHelper
{
    private static readonly string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test");

    public async Task<string> SaveFileAsync(string folderName, string fileName, Stream? stream, IFormFile? file, CancellationToken cancellationToken = default)
    {
        if (file == null && stream == null) throw new Exception("one must exist"); //TODO: ex
        if (file != null && stream != null) throw new Exception("one must null"); //TODO: ex

        string folderPath = Path.Combine(basePath, folderName);
        Directory.CreateDirectory(folderPath);

        if (File.Exists(Path.Combine(folderPath, fileName)))
        {
            fileName = Path.GetFileNameWithoutExtension(fileName) + "-Copy_" + Guid.NewGuid() + Path.GetExtension(fileName);
        }

        string filePath = Path.Combine(folderPath, fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);

        if (file != null)
            await file.CopyToAsync(fileStream, cancellationToken);
        else
            await stream!.CopyToAsync(fileStream, cancellationToken);
        return fileName;
    }

    public async Task<MemoryStream> GetFileAsync(string folderName, string fileName, CancellationToken cancellationToken = default)
    {
        string path = Path.Combine(basePath, folderName, fileName);
        if (!File.Exists(path)) throw new Exception("no file"); //TODO: exc

        var memoryStream = new MemoryStream();

        await using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
        {
            await fileStream.CopyToAsync(memoryStream, cancellationToken);
        }

        memoryStream.Position = 0;
        return memoryStream;
    }

    public async Task DeleteFileAsync(string folderName, string fileName)
    {
        string path = Path.Combine(basePath, folderName, fileName);
        if (!File.Exists(path)) throw new Exception("no file"); //TODO: exc

        await Task.Run(() => File.Delete(path));
    }
}