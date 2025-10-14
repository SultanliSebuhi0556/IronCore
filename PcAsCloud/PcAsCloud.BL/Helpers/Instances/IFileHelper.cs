namespace PcAsCloud.BL.Helpers.Instances;

public interface IFileHelper
{
    Task SaveFileAsync(string folderName, string newFileName, Stream file, CancellationToken cancellationToken);
    Task<MemoryStream> GetFileAsync(string folderName, string fileName, CancellationToken cancellationToken);
    Task DeleteFileAsync(string folderName, string fileName);
}
