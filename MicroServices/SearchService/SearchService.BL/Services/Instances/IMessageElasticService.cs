using SearchService.BL.DTOs.MessageDTOs;
using SearchService.CORE.Entities;

namespace SearchService.BL.Services.Instances;
public interface IMessageElasticService
{
    Task AddOrUpdateMessageAsync(Message message, CancellationToken cancellationToken);
    Task AddOrUpdateMessagesAsync(IEnumerable<Message> messages, CancellationToken cancellationToken);
    Task<IEnumerable<MessageSearchDTO>> GetAllBySearchAsync(string channelId, string? searchText, CancellationToken cancellationToken);
    Task RemoveAsync(string key, CancellationToken cancellationToken);
}