using AutoMapper;
using PcAsCloud.API.Features.Commands.UserCommands.UserLoginOrRegister;
using PcAsCloud.API.Features.Queries.UserQueries.UserGetAll;
using PcAsCloud.BL.DTOs.User;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.API.Profiles;
public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<UserLoginOrRegisterRequest, LoginDTO>();
        CreateMap<UserGetDTO, UserGetAllResponse>();
        CreateMap<LoginDTO, AppUser>();
        CreateMap<AppUser, UserGetDTO>();
    }
}