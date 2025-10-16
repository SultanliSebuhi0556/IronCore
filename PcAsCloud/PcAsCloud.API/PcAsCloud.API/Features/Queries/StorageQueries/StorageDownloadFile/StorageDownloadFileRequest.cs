using MediatR;

namespace PcAsCloud.API.Features.Queries.StorageQueries.StorageDownloadFile;
public class StorageDownloadFileRequest : IRequest<StorageDownloadFileResponse>
{
    public string FileName { get; set; }
}