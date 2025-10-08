using PcAsCloud.BL.DTOs.Message;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.Services.Services.Instances;

public interface IMessageService
{
    Task<string> CreateMessageAsync(AppUser currentUser, string channelId);
    Task<MessageGetDTO> GetMessageByIdAsync(string id);
    Task<IEnumerable<MessageGetDTO>> GetAllMessagesByChannelIdAsync(string channelId);
    Task DeleteMessageAsync(string id);
    Task ArchiveUnarchiveMessageAsync(string id);
}