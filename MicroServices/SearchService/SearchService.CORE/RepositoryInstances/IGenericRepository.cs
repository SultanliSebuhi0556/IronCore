using SearchService.CORE.Entities;

namespace SearchService.CORE.RepositoryInstances;
public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task CreateIndexIfNotExistAsync(string? indexName, CancellationToken cancellationToken);
    Task<bool> AddOrUpdateAsync(T entity, string? indexName, CancellationToken cancellationToken);
    Task<bool> AddOrUpdateRangeAsync(IEnumerable<T> entities, string? indexName, CancellationToken cancellationToken);
    Task<string> GetAsync(string channelId, string? searchText, string? indexName, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(string key, string? indexName, CancellationToken cancellationToken);
}