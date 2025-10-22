using AutoMapper;
using IronCore.BL.DTOs.Storage;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.StorageCommands.StorageDeleteFile;
public class StorageDeleteFileHandler(IStorageService _storageService, IMapper _mapper) : IRequestHandler<StorageDeleteFileRequest, StorageDeleteFileResponse>
{
    public async Task<StorageDeleteFileResponse> Handle(StorageDeleteFileRequest request, CancellationToken cancellationToken)
    {
        await _storageService.DeleteFileAsync(_mapper.Map<DeleteFileDTO>(request), cancellationToken);
        return new();
    }
}