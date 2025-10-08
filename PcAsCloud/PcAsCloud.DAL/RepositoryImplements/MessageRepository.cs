using PcAsCloud.CORE.Entities;
using PcAsCloud.CORE.RepositoryInstances;
using PcAsCloud.DAL.Context;

namespace PcAsCloud.DAL.RepositoryImplements;
public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(AppDbContext _context) : base(_context) { }
}