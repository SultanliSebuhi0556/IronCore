using IronCore.BL.DTOs.Storage;

namespace IronCore.BL.Services.Instances;

public interface IStorageService
{
    Task<UploadFileResultDTO> SaveFileAsync(UploadFileDTO dto, CancellationToken cancellationToken);
    Task<GetFileResultDTO> GetFileAsync(GetFileDTO dto, CancellationToken cancellationToken);
    Task DeleteFileAsync(DeleteFileDTO dto, CancellationToken cancellationToken);
}
