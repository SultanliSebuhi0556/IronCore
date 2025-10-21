using AutoMapper;
using MediatR;
using PcAsCloud.BL.DTOs.Storage;
using PcAsCloud.BL.Services.Services.Instances;

namespace PcAsCloud.API.Features.Commands.StorageCommands.StorageUploadFile;
public class StorageUploadFileHandler(IStorageService _storageService, IMapper _mapper) : IRequestHandler<StorageUploadFileRequest, StorageUploadFileResponse>
{
    public async Task<StorageUploadFileResponse> Handle(StorageUploadFileRequest request, CancellationToken cancellationToken)
    {
        var dto = await _storageService.SaveFileAsync(_mapper.Map<UploadFileDTO>(request), cancellationToken);
        return new() { StorageId = dto.StorageId.ToString(), FileName = dto.FileName };
    }
}