namespace PcAsCloud.API.Features.Queries.StorageQueries.StorageDownloadFile;
public class StorageDownloadFileResponse
{
    public MemoryStream Stream { get; set; }
    public string FileName { get; set; }
}