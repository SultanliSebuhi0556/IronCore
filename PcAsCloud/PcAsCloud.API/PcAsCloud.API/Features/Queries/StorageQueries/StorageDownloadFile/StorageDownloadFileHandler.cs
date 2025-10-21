using AutoMapper;
using MediatR;
using PcAsCloud.BL.DTOs.Storage;
using PcAsCloud.BL.Services.Services.Instances;

namespace PcAsCloud.API.Features.Queries.StorageQueries.StorageDownloadFile;
public class StorageDownloadFileHandler(IStorageService _storageService, IMapper _mapper) : IRequestHandler<StorageDownloadFileRequest, StorageDownloadFileResponse>
{
    public async Task<StorageDownloadFileResponse> Handle(StorageDownloadFileRequest request, CancellationToken cancellationToken)
    {
        var dto = await _storageService.GetFileAsync(_mapper.Map<GetFileDTO>(request), cancellationToken);
        return new() { Stream = dto.Stream, FileName = dto.FileName };
    }
}