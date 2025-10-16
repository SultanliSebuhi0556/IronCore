using PcAsCloud.BL.DTOs.Message;

namespace PcAsCloud.BL.Services.Instances;

public interface IMessageService
{
    Task<MessageCreateResponseDTO?> CreateMessageAsync(MessageCreateDTO dto, CancellationToken cancellationToken);
    Task<MessageGetDTO> GetMessageByIdAsync(string id);
    Task<IEnumerable<MessageGetDTO>> GetAllMessagesByChannelIdAsync(string channelId);
    Task DeleteMessageAsync(string id);
    Task ArchiveUnarchiveMessageAsync(string id);
}