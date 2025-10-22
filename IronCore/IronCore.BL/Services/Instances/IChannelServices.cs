using IronCore.BL.DTOs.Channel;

namespace IronCore.BL.Services.Instances;
public interface IChannelServices
{
    Task<string> CreateChannelAsync(ChannelCreateDTO dto, CancellationToken cancellationToken);
    Task LeaveChannelAsync(string id, CancellationToken cancellationToken);
    Task JoinChannelAsync(string id, CancellationToken cancellationToken);
    Task<ChannelGetDTO> GetChannelByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<ChannelGetDTO>> GetAllChannelsAsync(CancellationToken cancellationToken);
    Task DeleteChannelAsync(string id, CancellationToken cancellationToken);
    Task<bool> ArchiveUnarchiveChannelAsync(string id, CancellationToken cancellationToken);
}