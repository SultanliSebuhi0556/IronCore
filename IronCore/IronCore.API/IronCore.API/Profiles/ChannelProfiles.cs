using AutoMapper;
using IronCore.API.Features.Commands.ChannelCommands.ChannelCreate;
using IronCore.API.Features.Queries.ChannelQueries.ChannelGetAll;
using IronCore.BL.DTOs.Channel;
using IronCore.CORE.Entities;

namespace IronCore.API.Profiles;

public class ChannelProfiles : Profile
{
    public ChannelProfiles()
    {
        CreateMap<ChannelCreateRequest, ChannelCreateDTO>();
        CreateMap<ChannelGetDTO, ChannelGetAllResponse>();
        CreateMap<Channel, ChannelGetDTO>()
            .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.ChannelUsers.Select(u => u.AppUser.Id)));
    }
}
