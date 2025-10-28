using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using SearchService.CORE.Entities;
using SearchService.CORE.Options;
using SearchService.CORE.RepositoryInstances;
using System.Text.Json;

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

    public async Task<string> GetAsync(string channelId, string? searchText, string? indexName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(indexName)) indexName = "messages";

        var queries = new List<Query> { new MatchQuery { Field = "channelId.keyword", Query = channelId } };

        if (!string.IsNullOrWhiteSpace(searchText))
            queries.Add(new WildcardQuery { Field = "content.keyword", Value = $"*{searchText}*" });

        var request = new SearchRequest(indexName) { Query = new BoolQuery { Must = queries } };

        var response = await _client.SearchAsync<T>(request, cancellationToken);
        return JsonSerializer.Serialize(response.Documents);
    }

    public async Task<bool> RemoveAsync(string key, string? indexName, CancellationToken cancellationToken)
    {
        var response = await _client.DeleteAsync<T>(key, x => x.Index(indexName), cancellationToken);
        return response.IsValidResponse;
    }
}