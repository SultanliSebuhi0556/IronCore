namespace PcAsCloud.BL.ExternalServices.Instances;

public interface IFileHelper
{
    Task<string> SaveFileAsync(string folderName, string newFileName, Stream file, CancellationToken cancellationToken);
    Task<MemoryStream> GetFileAsync(string folderName, string fileName, CancellationToken cancellationToken);
    Task DeleteFileAsync(string folderName, string fileName);
}
