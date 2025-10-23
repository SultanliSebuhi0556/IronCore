using AutoMapper;
using IronCore.API.Features.Commands.UserCommands.UserLoginOrRegister;
using IronCore.API.Features.Commands.UserCommands.UserSetProfileImage;
using IronCore.API.Features.Queries.UserQueries.UserGetAll;
using IronCore.BL.DTOs.User;
using IronCore.CORE.Entities;

namespace IronCore.API.Profiles;
public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<UserSetProfileImageRequest, ChangeProfileImageDTO>();
        CreateMap<UserLoginOrRegisterRequest, LoginDTO>();
        CreateMap<UserGetDTO, UserGetAllResponse>();
        CreateMap<LoginDTO, AppUser>();
        CreateMap<AppUser, UserGetDTO>();
    }
}