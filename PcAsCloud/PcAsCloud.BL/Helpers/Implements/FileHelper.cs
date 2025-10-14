using PcAsCloud.BL.Helpers.Instances;

namespace PcAsCloud.BL.Helpers.Implements;
public class FileHelper : IFileHelper
{
    private static readonly string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test");
    public async Task SaveFileAsync(string folderName, string fileName, Stream file, CancellationToken cancellationToken = default)
    {
        string folderPath = Path.Combine(basePath, folderName);
        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, fileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
        await file.CopyToAsync(fileStream, cancellationToken);
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