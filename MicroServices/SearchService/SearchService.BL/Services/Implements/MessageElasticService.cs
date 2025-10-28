using AutoMapper;
using SearchService.BL.DTOs.MessageDTOs;
using SearchService.BL.Services.Instances;
using SearchService.CORE.Entities;
using SearchService.CORE.RepositoryInstances;
using System.Text.Json;

namespace SearchService.BL.Services.Implements;
public class MessageElasticService(IMessageRepository _repository, IMapper _mapper) : IMessageElasticService
{
    public const string IndexName = "messages";
    public async Task AddOrUpdateMessageAsync(Message message, CancellationToken cancellationToken)
        => await _repository.AddOrUpdateAsync(message, IndexName, cancellationToken);

    public async Task AddOrUpdateMessagesAsync(IEnumerable<Message> messages, CancellationToken cancellationToken)
        => await _repository.AddOrUpdateRangeAsync(messages, IndexName, cancellationToken);

    public async Task<IEnumerable<MessageSearchDTO>> GetAllBySearchAsync(string channelId, string? searchText, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAsync(channelId, searchText, IndexName, cancellationToken);
        return JsonSerializer.Deserialize<IEnumerable<MessageSearchDTO>>(result) ?? [];
    }
    public async Task RemoveAsync(string key, CancellationToken cancellationToken)
        => await _repository.RemoveAsync(key, IndexName, cancellationToken);
}