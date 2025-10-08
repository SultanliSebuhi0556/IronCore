using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.Services.Services.Instances;
public interface IChannelServices
{
    Task<string> CreateChannelAsync(AppUser currentUser, bool isDirect, string? channelName, AppUser? targertUser);
    Task<ChannelGetDTO> GetChannelByIdAsync(string id);
    Task<IEnumerable<ChannelGetDTO>> GetAllChannelsAsync();
    Task DeleteChannelAsync(string id);
    Task ArchiveUnarchiveChannelAsync(string id);
}