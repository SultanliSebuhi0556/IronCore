using SearchService.CORE.Entities;

namespace SearchService.CORE.RepositoryInstances;
public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task CreateIndexIfNotExistAsync(string indexName, CancellationToken cancellationToken);
    Task<bool> AddOrUpdateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> AddOrUpdateRangeAsync(IEnumerable<T> entities, string indexName, CancellationToken cancellationToken);
    Task<T> GetAsyncByKey(string key, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> RemoveAsync(string key, CancellationToken cancellationToken);
}