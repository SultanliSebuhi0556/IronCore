using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using SearchService.CORE.Entities;
using SearchService.CORE.Options;
using SearchService.CORE.RepositoryInstances;

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

    public async Task CreateIndexIfNotExistAsync(string? indexName, CancellationToken cancellationToken)
    {
        if (!_client.Indices.Exists(indexName).Exists)
            await _client.Indices.CreateAsync(indexName, cancellationToken);
    }

    public async Task<bool> AddOrUpdateAsync(T entity, string? indexName, CancellationToken cancellationToken)
    {
        var response = await _client.IndexAsync(entity, x => x.Index(indexName).OpType(OpType.Index), cancellationToken);
        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateRangeAsync(IEnumerable<T> entities, string? indexName, CancellationToken cancellationToken)
    {
        var response = await _client.BulkAsync(x => x.Index(indexName).UpdateMany(entities, (y, x) => y.Doc(x).DocAsUpsert(true)), cancellationToken);
        return response.IsValidResponse;
    }

    public async Task<IEnumerable<T>> GetAsync(string? searchText, string? indexName, CancellationToken cancellationToken)
    {
        var searchRequest = new SearchRequest(indexName)
        {
            Query = string.IsNullOrWhiteSpace(searchText) ? new MatchAllQuery() : new MatchQuery { Field = "searchText", Query = searchText }
        };
        var response = await _client.SearchAsync<T>(searchRequest, cancellationToken);
        return response.IsValidResponse ? response.Documents : Enumerable.Empty<T>();
    }

    public async Task<bool> RemoveAsync(string key, string? indexName, CancellationToken cancellationToken)
    {
        var response = await _client.DeleteAsync<T>(key, x => x.Index(indexName), cancellationToken);
        return response.IsValidResponse;
    }
}