using Microsoft.Extensions.Options;
using SearchService.CORE.Entities;
using SearchService.CORE.Options;
using SearchService.CORE.RepositoryInstances;

namespace SearchService.DAL.RepositoryImplements;
public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(IOptions<ElasticOptions> options) : base(options) { }
}