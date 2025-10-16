using MediatR;

namespace PcAsCloud.API.Features.Commands.StorageCommands.StorageDeleteFile;
public class StorageDeleteFileHandler : IRequestHandler<StorageDeleteFileRequest, StorageDeleteFileResponse>
{
    public Task<StorageDeleteFileResponse> Handle(StorageDeleteFileRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}