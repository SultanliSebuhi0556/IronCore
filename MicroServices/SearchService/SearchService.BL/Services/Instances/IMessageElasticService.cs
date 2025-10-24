using SearchService.CORE.Entities;

namespace SearchService.BL.Services.Instances;
public interface IMessageElasticService
{
    Task AddOrUpdateMessageAsync(Message message, CancellationToken cancellationToken);
    Task AddOrUpdateMessagesAsync(IEnumerable<Message> messages, CancellationToken cancellationToken);
    Task<IEnumerable<Message>> GetAllAsync(string? searchText, CancellationToken cancellationToken);
    Task RemoveAsync(string key, CancellationToken cancellationToken);
}