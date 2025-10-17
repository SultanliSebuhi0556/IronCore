using MediatR;

namespace PcAsCloud.API.Features.Queries.StorageQueries.StorageDownloadFile;
public class StorageDownloadFileHandler : IRequestHandler<StorageDownloadFileRequest, StorageDownloadFileResponse>
{
    public Task<StorageDownloadFileResponse> Handle(StorageDownloadFileRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}