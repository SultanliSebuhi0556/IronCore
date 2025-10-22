using IronCore.BL.DTOs.Message;

namespace IronCore.BL.Services.Instances;

public interface IMessageService
{
    Task<MessageCreateResponseDTO?> CreateMessageAsync(MessageCreateDTO dto, CancellationToken cancellationToken);
    Task<MessageGetDTO> GetMessageByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MessageGetDTO>> GetAllMessagesByChannelIdAsync(string channelId, CancellationToken cancellationToken);
    Task DeleteMessageAsync(string id, CancellationToken cancellationToken);
    Task<bool> ArchiveUnarchiveMessageAsync(string id, CancellationToken cancellationToken);
}