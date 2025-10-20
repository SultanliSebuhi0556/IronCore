using PcAsCloud.BL.DTOs.Channel;

namespace PcAsCloud.BL.Services.Instances;
public interface IChannelServices
{
    Task<string> CreateChannelAsync(ChannelCreateDTO dto);
    Task LeaveChannelAsync(string id);
    Task JoinChannelAsync(string id);
    Task<ChannelGetDTO> GetChannelByIdAsync(string id);
    Task<IEnumerable<ChannelGetDTO>> GetAllChannelsAsync();
    Task DeleteChannelAsync(string id);
    Task<bool> ArchiveUnarchiveChannelAsync(string id);
}