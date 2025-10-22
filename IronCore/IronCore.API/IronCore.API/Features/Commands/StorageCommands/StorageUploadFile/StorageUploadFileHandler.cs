using AutoMapper;
using IronCore.BL.DTOs.Storage;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.StorageCommands.StorageUploadFile;
public class StorageUploadFileHandler(IStorageService _storageService, IMapper _mapper) : IRequestHandler<StorageUploadFileRequest, StorageUploadFileResponse>
{
    public async Task<StorageUploadFileResponse> Handle(StorageUploadFileRequest request, CancellationToken cancellationToken)
    {
        var dto = await _storageService.SaveFileAsync(_mapper.Map<UploadFileDTO>(request), cancellationToken);
        return new() { StorageId = dto.StorageId.ToString(), FileName = dto.FileName };
    }
}