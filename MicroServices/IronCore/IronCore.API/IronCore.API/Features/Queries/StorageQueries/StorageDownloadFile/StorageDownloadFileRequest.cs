using MediatR;

namespace IronCore.API.Features.Queries.StorageQueries.StorageDownloadFile;
public class StorageDownloadFileRequest : IRequest<StorageDownloadFileResponse>
{
    public string StorageId { get; set; }
    public string? ChannelId { get; set; }
}