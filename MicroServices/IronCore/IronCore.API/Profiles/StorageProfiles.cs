using AutoMapper;
using IronCore.API.Features.Commands.StorageCommands.StorageDeleteFile;
using IronCore.API.Features.Commands.StorageCommands.StorageUploadFile;
using IronCore.API.Features.Queries.StorageQueries.StorageDownloadFile;
using IronCore.BL.DTOs.Storage;

namespace IronCore.API.Profiles;
public class StorageProfiles : Profile
{
    public StorageProfiles()
    {
        CreateMap<StorageUploadFileRequest, UploadFileDTO>();
        CreateMap<StorageDeleteFileRequest, DeleteFileDTO>();
        CreateMap<StorageDownloadFileRequest, GetFileDTO>();
    }
}