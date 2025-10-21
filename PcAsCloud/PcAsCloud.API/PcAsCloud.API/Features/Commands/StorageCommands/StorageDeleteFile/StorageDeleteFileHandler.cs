using AutoMapper;
using MediatR;
using PcAsCloud.BL.DTOs.Storage;
using PcAsCloud.BL.Services.Services.Instances;

namespace PcAsCloud.API.Features.Commands.StorageCommands.StorageDeleteFile;
public class StorageDeleteFileHandler(IStorageService _storageService, IMapper _mapper) : IRequestHandler<StorageDeleteFileRequest, StorageDeleteFileResponse>
{
    public async Task<StorageDeleteFileResponse> Handle(StorageDeleteFileRequest request, CancellationToken cancellationToken)
    {
        await _storageService.DeleteFileAsync(_mapper.Map<DeleteFileDTO>(request), cancellationToken);
        return new();
    }
}