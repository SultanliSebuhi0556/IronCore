using AutoMapper;
using IronCore.BL.DTOs.Storage;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Queries.StorageQueries.StorageDownloadFile;
public class StorageDownloadFileHandler(IStorageService _storageService, IMapper _mapper) : IRequestHandler<StorageDownloadFileRequest, StorageDownloadFileResponse>
{
    public async Task<StorageDownloadFileResponse> Handle(StorageDownloadFileRequest request, CancellationToken cancellationToken)
    {
        var dto = await _storageService.GetFileAsync(_mapper.Map<GetFileDTO>(request), cancellationToken);
        return new() { Stream = dto.Stream, FileName = dto.FileName };
    }
}