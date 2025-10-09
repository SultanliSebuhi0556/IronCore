using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.Services.Services.Instances;
public interface IChannelServices
{
    Task<string> CreateChannelAsync(ChannelCreateDTO dto);
    Task<Channel> GetChannelByIdAsync(string id);
    Task<IEnumerable<Channel>> GetAllChannelsAsync();
    Task DeleteChannelAsync(string id);
    Task ArchiveUnarchiveChannelAsync(string id);
}