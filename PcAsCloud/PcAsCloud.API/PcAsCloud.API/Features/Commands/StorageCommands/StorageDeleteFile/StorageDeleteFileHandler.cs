using MediatR;
using PcAsCloud.BL.Services.Services.Instances;

namespace PcAsCloud.API.Features.Commands.StorageCommands.StorageDeleteFile;
public class StorageDeleteFileHandler(IStorageService _storageService) : IRequestHandler<StorageDeleteFileRequest, StorageDeleteFileResponse>
{
    public async Task<StorageDeleteFileResponse> Handle(StorageDeleteFileRequest request, CancellationToken cancellationToken)
    {
        await _storageService.DeleteFileAsync(request.FileName);
        return new();
    }
}