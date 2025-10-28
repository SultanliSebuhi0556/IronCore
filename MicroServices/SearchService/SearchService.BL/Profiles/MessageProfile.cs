using AutoMapper;
using SearchService.BL.DTOs.MessageDTOs;
using SearchService.CORE.Entities;

namespace SearchService.BL.Profiles;
public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageSearchDTO>();
    }
}