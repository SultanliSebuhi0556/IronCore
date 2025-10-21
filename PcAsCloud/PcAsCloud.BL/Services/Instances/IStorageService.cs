using PcAsCloud.BL.DTOs.Storage;

namespace PcAsCloud.BL.Services.Services.Instances;

public interface IStorageService
{
    Task<UploadFileResultDTO> SaveFileAsync(UploadFileDTO dto, CancellationToken cancellationToken);
    Task<GetFileResultDTO> GetFileAsync(GetFileDTO dto, CancellationToken cancellationToken);
    Task DeleteFileAsync(DeleteFileDTO dto, CancellationToken cancellationToken);
}
