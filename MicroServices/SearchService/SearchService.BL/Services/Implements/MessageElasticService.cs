using SearchService.BL.Services.Instances;
using SearchService.CORE.Entities;
using SearchService.CORE.RepositoryInstances;

namespace SearchService.BL.Services.Implements;
public class MessageElasticService(IMessageRepository _repository) : IMessageElasticService
{
    public const string IndexName = "messages";
    public async Task AddOrUpdateMessageAsync(Message message, CancellationToken cancellationToken)
        => await _repository.AddOrUpdateAsync(message, IndexName, cancellationToken);

    public async Task AddOrUpdateMessagesAsync(IEnumerable<Message> messages, CancellationToken cancellationToken)
        => await _repository.AddOrUpdateRangeAsync(messages, IndexName, cancellationToken);

    public async Task<IEnumerable<Message>> GetAllAsync(string? searchText, CancellationToken cancellationToken)
        => await _repository.GetAsync(searchText, IndexName, cancellationToken);

    public async Task RemoveAsync(string key, CancellationToken cancellationToken)
        => await _repository.RemoveAsync(key, IndexName, cancellationToken);
}