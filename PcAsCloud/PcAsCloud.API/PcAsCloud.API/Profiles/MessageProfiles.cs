using AutoMapper;
using PcAsCloud.BL.DTOs.Message;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.API.Profiles;
public class MessageProfiles : Profile
{
    public MessageProfiles()
    {
        CreateMap<Message, MessageGetDTO>();
    }
}