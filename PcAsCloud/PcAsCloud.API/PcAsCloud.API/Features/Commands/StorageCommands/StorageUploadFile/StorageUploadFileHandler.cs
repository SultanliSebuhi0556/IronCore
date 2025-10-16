using MediatR;

namespace PcAsCloud.API.Features.Commands.StorageCommands.StorageUploadFile;
public class StorageUploadFileHandler : IRequestHandler<StorageUploadFileRequest, StorageUploadFileResponse>
{
    public Task<StorageUploadFileResponse> Handle(StorageUploadFileRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}