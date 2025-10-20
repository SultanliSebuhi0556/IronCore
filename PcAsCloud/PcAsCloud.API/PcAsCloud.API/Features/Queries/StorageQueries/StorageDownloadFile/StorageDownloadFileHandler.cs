using MediatR;
using PcAsCloud.BL.Services.Services.Instances;

namespace PcAsCloud.API.Features.Queries.StorageQueries.StorageDownloadFile;
public class StorageDownloadFileHandler(IStorageService _storageService) : IRequestHandler<StorageDownloadFileRequest, StorageDownloadFileResponse>
{
    public async Task<StorageDownloadFileResponse> Handle(StorageDownloadFileRequest request, CancellationToken cancellationToken)
    {
        var stream = await _storageService.GetFileAsync(request.FileName, cancellationToken);
        return new() { Stream = stream };
    }
}