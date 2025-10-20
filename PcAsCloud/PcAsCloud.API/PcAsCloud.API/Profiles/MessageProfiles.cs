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
        CreateMap<MessageGetDTO, MessageGetChannelMessagesResponse>();
        CreateMap<MessageCreateRequest, MessageCreateDTO>();
        CreateMap<Message, MessageGetDTO>();
    }
}