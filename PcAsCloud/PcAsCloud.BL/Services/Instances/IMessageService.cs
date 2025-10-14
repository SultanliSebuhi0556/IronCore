using PcAsCloud.BL.DTOs.Message;

namespace PcAsCloud.BL.Services.Instances;

public interface IMessageService
{
    Task<string?> CreateMessageAsync(MessageCreateDTO dto, string rootPath);
    Task<MessageGetDTO> GetMessageByIdAsync(string id);
    Task<IEnumerable<MessageGetDTO>> GetAllMessagesByChannelIdAsync(string channelId);
    Task DeleteMessageAsync(string id);
    Task ArchiveUnarchiveMessageAsync(string id);
}