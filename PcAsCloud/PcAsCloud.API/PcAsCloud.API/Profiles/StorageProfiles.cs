using AutoMapper;
using PcAsCloud.API.Features.Commands.StorageCommands.StorageUploadFile;
using PcAsCloud.BL.DTOs.Storage;

namespace PcAsCloud.API.Profiles;
public class StorageProfiles : Profile
{
    public StorageProfiles()
    {
        CreateMap<StorageUploadFileRequest, UploadFileDTO>();
    }
}