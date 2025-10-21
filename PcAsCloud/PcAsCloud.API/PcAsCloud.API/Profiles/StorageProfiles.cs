using AutoMapper;
using PcAsCloud.API.Features.Commands.StorageCommands.StorageDeleteFile;
using PcAsCloud.API.Features.Commands.StorageCommands.StorageUploadFile;
using PcAsCloud.API.Features.Queries.StorageQueries.StorageDownloadFile;
using PcAsCloud.BL.DTOs.Storage;

namespace PcAsCloud.API.Profiles;
public class StorageProfiles : Profile
{
    public StorageProfiles()
    {
        CreateMap<StorageUploadFileRequest, UploadFileDTO>();
        CreateMap<StorageDeleteFileRequest, DeleteFileDTO>();
        CreateMap<StorageDownloadFileRequest, GetFileDTO>();
    }
}