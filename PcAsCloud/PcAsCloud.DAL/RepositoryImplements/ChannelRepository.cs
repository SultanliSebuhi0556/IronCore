using PcAsCloud.CORE.Entities;
using PcAsCloud.CORE.RepositoryInstances;
using PcAsCloud.DAL.Context;

namespace PcAsCloud.DAL.RepositoryImplements;
public class ChannelRepository : GenericRepository<Channel>, IChannelRepository
{
    public ChannelRepository(AppDbContext _context) : base(_context) { }
}