using AutoMapper;
using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.Profiles;

public class ChannelProfiles : Profile
{
    public ChannelProfiles()
    {
        CreateMap<Channel, ChannelGetDTO>()
            .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.ChannelUsers.Select(u => u.User.Id)));
    }
}
