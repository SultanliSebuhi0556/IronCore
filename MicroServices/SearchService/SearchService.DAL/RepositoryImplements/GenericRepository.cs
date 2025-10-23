using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using SearchService.CORE.Entities;
using SearchService.CORE.RepositoryInstances;
using SearchService.DAL.Options;

namespace SearchService.DAL.RepositoryImplements;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticOptions _options;

    public GenericRepository(IOptions<ElasticOptions> options)
    {
        _options = options.Value;
        var settings = new ElasticsearchClientSettings(new Uri(_options.Url))
            .Authentication(new BasicAuthentication(_options.Username, _options.Password))
            .DefaultIndex(_options.DefaultIndex);

        _client = new ElasticsearchClient(settings);
    }

    public async Task CreateIndexIfNotExistAsync(string indexName, CancellationToken cancellationToken)
    {
        if (!_client.Indices.Exists(indexName).Exists)
            await _client.Indices.CreateAsync(indexName, cancellationToken);
    }

    public async Task<bool> AddOrUpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var response = await _client.IndexAsync(entity, x => x.Index(_options.DefaultIndex).OpType(OpType.Index), cancellationToken);
        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateRangeAsync(IEnumerable<T> entities, string indexName, CancellationToken cancellationToken)
    {
        var response = await _client.BulkAsync(x => x.Index(_options.DefaultIndex).UpdateMany(entities, (y, x) => y.Doc(x).DocAsUpsert(true)), cancellationToken);
        return response.IsValidResponse;
    }

    public async Task<T> GetAsyncByKey(string key, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync<T>(key, x => x.Index(_options.DefaultIndex), cancellationToken);
        return response.Source;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        var response = await _client.SearchAsync<T>(x => x.Indices(_options.DefaultIndex), cancellationToken);
        return response.IsValidResponse ? response.Documents : default;
    }

    public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken)
    {
        var response = await _client.DeleteAsync<T>(key, x => x.Index(_options.DefaultIndex), cancellationToken);
        return response.IsValidResponse;
    }
}