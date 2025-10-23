using Microsoft.Extensions.Options;
using SearchService.CORE.Entities;
using SearchService.CORE.RepositoryInstances;
using SearchService.DAL.Options;

namespace SearchService.DAL.RepositoryImplements;
public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(IOptions<ElasticOptions> options) : base(options) { }
}