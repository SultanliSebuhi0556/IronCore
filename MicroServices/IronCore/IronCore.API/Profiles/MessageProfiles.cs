using AutoMapper;
using IronCore.API.Features.Commands.MessageCommands.MessageCreate;
using IronCore.API.Features.Queries.MessageQueries.MessageGetAllBySearch;
using IronCore.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
using IronCore.BL.DTOs.Message;
using IronCore.CORE.Entities;

namespace IronCore.API.Profiles;
public class MessageProfiles : Profile
{
    public MessageProfiles()
    {
        CreateMap<MessageGetDTO, MessageGetAllBySearchResult>();
        CreateMap<MessageCreateRequest, MessageCreateDTO>();
        CreateMap<Message, MessageGetDTO>();
        CreateMap<MessageGetDTO, MessageGetChannelMessagesResponse>()
            .ForMember(dest => dest.SendedByUserId, opt => opt.MapFrom(src => src.SendedById));
    }
}