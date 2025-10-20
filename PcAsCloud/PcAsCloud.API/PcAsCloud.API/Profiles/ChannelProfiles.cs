using AutoMapper;
using PcAsCloud.API.Features.Commands.ChannelCommands.ChannelCreate;
using PcAsCloud.API.Features.Queries.ChannelQueries.ChannelGetAll;
using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.API.Profiles;

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
