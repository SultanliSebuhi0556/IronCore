using AutoMapper;
using PcAsCloud.API.Features.Commands.MessageCommands.MessageCreate;
using PcAsCloud.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
using PcAsCloud.BL.DTOs.Message;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.API.Profiles;
public class MessageProfiles : Profile
{
    public MessageProfiles()
    {
        CreateMap<MessageCreateRequest, MessageCreateDTO>();
        CreateMap<Message, MessageGetDTO>();
        CreateMap<MessageGetDTO, MessageGetChannelMessagesResponse>()
            .ForMember(dest => dest.SendedByUserId, opt => opt.MapFrom(src => src.SendedById));
    }
}