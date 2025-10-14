using AutoMapper;
using PcAsCloud.BL.DTOs.User;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.Profiles;
public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<LoginDTO, AppUser>();
        CreateMap<AppUser, UserGetDTO>();
    }
}